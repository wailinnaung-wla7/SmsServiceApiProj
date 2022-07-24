using Am.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Am.Infrastructure.IRepositories
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        Task<RefreshToken> AddAsync(RefreshToken model);
        Task<RefreshToken> GetAsync(string token);
        Task<bool> UpdateRevokedTokenAsync(RefreshToken model,string reason, string newToken=null);
        Task<bool> UpdateCompromisedTokensAsync(RefreshToken model, string reason);
        Task<bool> DeleteAsync(string ServiceCode, int Days);
    }
}
