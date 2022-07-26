using Am.Infrastructure.Dto.SmsService;
using Am.Infrastructure.Entities;
using Am.Infrastructure.IRepositories;
using Am.Infrastructure.IServices;
using Am.Service.Helpers;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Am.Service.Services
{
    public class SmsTransactionService : ISmsTransactionService
    {
        private readonly ISmsTransactionRepository _smsTransactionRepository;
        private readonly IMapper _mapper;

        public SmsTransactionService(ISmsTransactionRepository smsTransactionRepository,IMapper mapper)
        {
            _smsTransactionRepository = smsTransactionRepository;
            _mapper = mapper;
        }
        public async Task<bool> CreateSMSTransactions(SendBulkSmsRequestDTO requestModel, List<string> FailedNumbers, string ServiceCode)
        {
            var populatedModel = General.PopulateSmsTransaction(requestModel, FailedNumbers, ServiceCode);
            var models = _mapper.Map<List<SmsTransaction>>(populatedModel);
            return await _smsTransactionRepository.CreateSmsTransactionAsync(models);
        }
        public async Task<int> GetSmsTransactionsForToday(string ServiceCode)
        {
            var tranLst = await _smsTransactionRepository.GetTransactionsByServiceCodeAsync(ServiceCode);
            return tranLst.Count();
        }
    }
}
