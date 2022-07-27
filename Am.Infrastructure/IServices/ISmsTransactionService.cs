using Am.Infrastructure.Dto.Pagination;
using Am.Infrastructure.Dto.SmsService;
using Am.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Am.Infrastructure.IServices
{
    public interface ISmsTransactionService
    {
        Task<bool> CreateSMSTransactions(SendBulkSmsRequestDTO requestModel, List<string> FailedNumbers, string ServiceCode);
        Task<int> GetSmsTransactionsForToday(string ServiceCode);
        Task<PagedObjectList<SmsTransaction>> GetSmsTransactions(SmsTransactionParameters parameters);
    }
}
