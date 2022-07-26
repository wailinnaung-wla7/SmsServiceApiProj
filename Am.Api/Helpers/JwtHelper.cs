using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Am.Api.Helpers
{
    // To Ask about static class usage (DI)
    public static class JwtHelper
    {
        private static IConfiguration _config;
        public static void JWTClaimHelperConfigure(IConfiguration config)
        {
            _config = config;
        }
        public static string GetServiceCodeFromJwtToken(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var ServiceCode = ValidateToken(token);
            return ServiceCode;
        }
        private static string? ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["JWT:Secret"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = _config["JWT:ValidAudience"],
                    ValidIssuer = _config["JWT:ValidIssuer"],
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var ServiceCode = jwtToken.Claims.First(x => x.Type == "ServiceCode").Value.ToString();

                // return Service Code from JWT token if validation successful
                return ServiceCode;
            }
            catch
            {
                return null;
            }
        }
    }
}
