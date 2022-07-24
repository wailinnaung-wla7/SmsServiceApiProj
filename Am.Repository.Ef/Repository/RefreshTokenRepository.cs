using Am.Infrastructure.Entities;
using Am.Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Am.Repository.Ef.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<RefreshToken> AddAsync(RefreshToken model)
        {
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> UpdateRevokedTokenAsync(RefreshToken model,string reason,string newToken=null)
        {
            var existingToken = await _context.RefreshToken.FindAsync(model.Id);
            existingToken.Revoked = DateTime.UtcNow;
            existingToken.ReasonRevoked = reason;
            existingToken.ReplacedByToken = newToken;
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }
        public async Task<bool> UpdateCompromisedTokensAsync(RefreshToken model,string reason)
        {
            var tokens = await _context.RefreshToken.Where(a => a.ServiceCode == model.ServiceCode && a.Id > model.Id).ToListAsync();
            foreach(var token in tokens)
            {
                token.Revoked = DateTime.UtcNow;
                token.ReasonRevoked = reason;
            }
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
            
        }
        public async Task<RefreshToken> GetAsync(string token)
        {
            return await _context.RefreshToken.Where(a=>a.Token == token).FirstOrDefaultAsync();
        }
        public async Task<bool> DeleteAsync(string ServiceCode,int Days)
        {
            var FilterDatetime = DateTime.UtcNow.AddDays(-Days);
            var oldTokens = await _context.RefreshToken.Where(a => a.ServiceCode == ServiceCode && a.IsActive == false && a.CreatedDate <= FilterDatetime).ToListAsync();
            _context.RemoveRange(oldTokens);
            var Deleted = await _context.SaveChangesAsync();
            return Deleted > 0;
        }
    }
}
