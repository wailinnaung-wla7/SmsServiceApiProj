using Am.Infrastructure.Dto.AuthenticationService;
using Am.Infrastructure.Entities;
using Am.Infrastructure.IRepositories;
using Am.Infrastructure.IServices;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Am.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Private
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        #endregion
        public AuthenticationService(IMapper mapper, IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository)
        {
            _mapper = mapper;
            _configuration = configuration;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public JwtSecurityToken CreateToken(string ServiceCode)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);
            var authClaims = new List<Claim>
            {
                new Claim("ServiceCode", ServiceCode)
            };

            var token = new JwtSecurityToken(
               issuer: _configuration["JWT:ValidIssuer"],
               audience: _configuration["JWT:ValidAudience"],
               expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
               claims: authClaims,
               signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
               );

            return token;
        }

        public async Task<string> GenerateRefreshToken(string ServiceCode)
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            // ensure token is unique by checking against db
            var refreshtoken = await _refreshTokenRepository.GetAsync(token);
            if (refreshtoken != null)
            {
                await GenerateRefreshToken(ServiceCode);
            }
            var refreshToken = new RefreshToken
            {
                Token = token,
                // token is valid for 7 days
                Expires = DateTime.UtcNow.AddDays(7),
                CreatedDate = DateTime.UtcNow,
                ServiceCode = ServiceCode
            };
            await _refreshTokenRepository.AddAsync(refreshToken);
            return token;

        }
        public async Task<AuthenticationResponse> RefreshToken(RefreshToken tokenInfo)
        {
            var newRefreshToken = await rotateRefreshToken(tokenInfo);

            // remove old refresh tokens from user
            await removeOldRefreshTokens(tokenInfo);

            // generate new jwt
            var jwtToken = CreateToken(tokenInfo.ServiceCode);

            return new AuthenticationResponse { Token = new JwtSecurityTokenHandler().WriteToken(jwtToken), RefreshToken = newRefreshToken, Expiration = jwtToken.ValidTo };
        }
        public async Task<RefreshToken> CheckRefreshToken(string RefreshToken)
        {
            var refreshTokenInfo = await _refreshTokenRepository.GetAsync(RefreshToken);
            if (refreshTokenInfo.IsRevoked)
            {
                // revoke all descendant tokens in case this token has been compromised
                await _refreshTokenRepository.UpdateCompromisedTokensAsync(refreshTokenInfo, $"Attempted reuse of revoked ancestor token: {RefreshToken}");
                return null;
            }
            return refreshTokenInfo;
        }
        public async Task<bool> RevokeRefreshToken(RefreshToken token)
        {
            await _refreshTokenRepository.UpdateRevokedTokenAsync(token, "Revoked without replacement(Log Out)");
            return true;
        }
        #region Helpers
        private async Task<string> rotateRefreshToken(RefreshToken refreshToken)
        {
            var newRefreshToken = await GenerateRefreshToken(refreshToken.ServiceCode);
            await _refreshTokenRepository.UpdateRevokedTokenAsync(refreshToken, "Replaced by new token", newRefreshToken);
            return newRefreshToken;
        }
        private async Task<bool> removeOldRefreshTokens(RefreshToken token)
        {
            // remove old inactive refresh tokens from user based on TTL in app settings
            var KeepTokenDays = Convert.ToInt32(_configuration["AppSettings:RefreshTokenTTL"]);
            await _refreshTokenRepository.DeleteAsync(token.ServiceCode, KeepTokenDays);
            return true;
        }
        #endregion 
    }
}
