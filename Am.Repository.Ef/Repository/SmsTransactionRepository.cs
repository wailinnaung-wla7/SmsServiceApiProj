using Am.Infrastructure.Dto.Pagination;
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
    public class SmsTransactionRepository : ISmsTransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public SmsTransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateSmsTransactionAsync(List<SmsTransaction> smsTransactions)
        {
            foreach (var model in smsTransactions)
            {
                model.CreatedDate = DateTime.UtcNow;
                model.IsActive = true;
            }
            await _context.AddRangeAsync(smsTransactions);
            var created = await _context.SaveChangesAsync();
            return created > 0;
        }

        public async Task<List<SmsTransaction>> GetAllTransactionsAsync()
        {
            return await _context.SmsTransaction.ToListAsync();
        }

        public async Task<List<SmsTransaction>> GetTransactionsByServiceCodeAsync(string ServiceCode)
        {
            return await _context.SmsTransaction.Where(a => a.ServiceCode == ServiceCode && a.CreatedDate.Date == DateTime.UtcNow.Date).ToListAsync();
        }
        public async Task<PagedObjectList<SmsTransaction>> GetSmsTransactions(SmsTransactionParameters smsTransactionParameters)
        {
            return PagedObjectList<SmsTransaction>.ToPagedList(
                smsTransactionParameters.ServiceCode != string.Empty ?
                _context.SmsTransaction.Where(a => a.ServiceCode == smsTransactionParameters.ServiceCode).OrderBy(o=>o.CreatedDate) :
                _context.SmsTransaction.OrderBy(on => on.CreatedDate),
                smsTransactionParameters.PageNumber,
                smsTransactionParameters.PageSize);
        }
    }
}
