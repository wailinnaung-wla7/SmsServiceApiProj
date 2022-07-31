using Am.Infrastructure.Dto.AuthenticationService;
using Am.Infrastructure.IServices;
using Am.Service.ServiceManager;
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
        private readonly ILogger<AuthenticationController> _logger;
        private readonly ISmsService _smsService;
        private readonly AuthenticationServiceManager _authenticationServiceManager;
        #endregion

        // TODO: Add Centralize Logging
        public AuthenticationController(AuthenticationServiceManager authenticationServiceManager,
            ILogger<AuthenticationController> logger)
        {
            _authenticationServiceManager = authenticationServiceManager;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationServiceRequestDTO request)
        {
            var result = await _authenticationServiceManager.Authenticate(request);
            _logger.LogInformation("This is a log message. This is an object: {User}", new { name = "John Doe" });
            return Ok(result);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestDTO request)
        {
            var response = await _authenticationServiceManager.RefreshToken(request);
            return Ok(response);
        }
        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken(RevokeTokenRequestDTO request)
        {
            var resopnse = await _authenticationServiceManager.RevokeToken(request);
            return Ok(new { message = "Token revoked" });
        }
    }
}
