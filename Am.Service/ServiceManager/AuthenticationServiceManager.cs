using Am.Infrastructure.Dto.AuthenticationService;
using Am.Infrastructure.IServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Am.Service.ServiceManager
{
    public class AuthenticationServiceManager
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<AuthenticationServiceManager> _logger;
        private readonly ISmsService _smsService;

        public AuthenticationServiceManager(IAuthenticationService authenticationService,
            ILogger<AuthenticationServiceManager> logger, ISmsService smsService)
        {
            _authenticationService = authenticationService;
            _logger = logger;
            _smsService = smsService;
        }

        public async Task<AuthenticationResponse>Authenticate(AuthenticationServiceRequestDTO request)
        {
            var Service = await _smsService.GetAsync(request.Code);
            //if (Service == null)
            //{ throw new Exception(); }
            var token = _authenticationService.CreateToken(request.Code);
            var refreshToken = await _authenticationService.GenerateRefreshToken(request.Code);
            return new AuthenticationResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            };
        }
        public async Task<AuthenticationResponse> RefreshToken(RefreshTokenRequestDTO request)
        {
            var tokenInfo = await _authenticationService.CheckRefreshToken(request.RefreshToken);
            if (tokenInfo == null || !tokenInfo.IsActive)
                throw new Exception();

            return await _authenticationService.RefreshToken(tokenInfo);
        }
        public async Task<bool> RevokeToken(RevokeTokenRequestDTO request)
        {
            var tokenInfo = await _authenticationService.CheckRefreshToken(request.Token);
            if (tokenInfo == null || !tokenInfo.IsActive)
                throw new Exception();

            bool IsSuccess= await _authenticationService.RevokeRefreshToken(tokenInfo);
            if (!IsSuccess)
            { throw new Exception(); }
            return IsSuccess;
        }
    }
}
