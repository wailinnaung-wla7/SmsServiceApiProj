using Am.Infrastructure.Dto.AuthenticationService;
using Am.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Am.Infrastructure.IServices
{
    public interface IAuthenticationService
    {
        JwtSecurityToken CreateToken(string ServiceCode);
        Task<string> GenerateRefreshToken(string ServiceCode);
        Task<RefreshToken> CheckRefreshToken(string RefreshToken);
        Task<AuthenticationResponse> RefreshToken(RefreshToken token);

        Task<bool> RevokeRefreshToken(RefreshToken token);

    }
}
