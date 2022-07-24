﻿using Am.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Am.Infrastructure.IRepositories
{
    public interface ISmsTransactionRepository : IRepository<SmsTransaction>
    {

        Task<List<SmsTransaction>> GetAllTransactionsAsync();
        Task<List<SmsTransaction>> GetTransactionsByServiceIdAsync(long id);
        Task<bool> CreateSmsTransactionAsync(List<SmsTransaction> smsTransactions);

    }
}
