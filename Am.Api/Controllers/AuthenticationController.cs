using Am.Infrastructure.Dto.AuthenticationService;
using Am.Infrastructure.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Am.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        #region Private
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<SmsServiceController> _logger;
        private readonly ISmsService _smsService;
        #endregion

        // TODO: Add Centralize Logging
        public AuthenticationController(IAuthenticationService authenticationService,
            ILogger<SmsServiceController> logger,ISmsService smsService)
        {
            _authenticationService = authenticationService;
            _logger = logger;
            _smsService = smsService;
        }
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationServiceRequestDTO request)
        {
            var Service = await _smsService.GetAsync(request.Code);
            if (Service == null)
            {
                return Unauthorized();
            }
            var token = _authenticationService.CreateToken(request.Code);
            var refreshToken = await _authenticationService.GenerateRefreshToken(request.Code);
            return Ok(new AuthenticationResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            });
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestDTO request)
        {
            var tokenInfo = await _authenticationService.CheckRefreshToken(request.RefreshToken);
            if (tokenInfo == null || !tokenInfo.IsActive)
               return BadRequest("Invalid Token");

            var response = await _authenticationService.RefreshToken(tokenInfo);
            return Ok(response);
        }
        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken(RevokeTokenRequestDTO request)
        {
            var tokenInfo = await _authenticationService.CheckRefreshToken(request.Token);
            if (tokenInfo == null || !tokenInfo.IsActive)
                BadRequest("Invalid Token");

            await _authenticationService.RevokeRefreshToken(tokenInfo);
            return Ok(new { message = "Token revoked" });
        }
    }
}
