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
            throw new NotImplementedException();
        }

        public async Task<List<SmsTransaction>> GetAllTransactionsAsync()
        {
            return await _context.SmsTransaction.ToListAsync();
        }

        public async Task<List<SmsTransaction>> GetTransactionsByServiceIdAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}
