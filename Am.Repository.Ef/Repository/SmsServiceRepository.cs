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
    public class SmsServiceRepository : ISmsServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public SmsServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<SmsServiceEntity> AddAsync(SmsServiceEntity model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<SmsServiceEntity> GetAsync(long id)
        {
            return await _context.Service.FindAsync(id);
        }
        public async Task<SmsServiceEntity> GetAsync(string Code)
        {
            return await _context.Service.Where(x => x.Code == Code).FirstOrDefaultAsync();
        }
    }
}
