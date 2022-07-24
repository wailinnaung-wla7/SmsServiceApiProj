using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Am.Infrastructure.Dto.AuthenticationService
{
    public class AuthenticationServiceRequestDTO
    {
        public string Code { get; set; }
    }
    public class RefreshTokenRequestDTO
    {
        public string RefreshToken { get; set; }
    }
}
