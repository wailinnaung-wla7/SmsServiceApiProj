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
    public class SmsService : ISmsService
    {
        #region Private
        private readonly IMapper _mapper;
        private readonly ISmsServiceRepository _smsServiceRepository;
        #endregion
        public SmsService(ISmsServiceRepository smsServiceRepository,
            IMapper mapper)
        {

            _smsServiceRepository = smsServiceRepository;
            _mapper = mapper;
        }

        //public async Task<long> CreateSmsService(SmsServiceCreateDTO smsService)
        public async Task<SmsServiceGetResponseDTO> CreateSmsService(SmsServiceCreateDTO smsService)
        {
            var model = _mapper.Map<SmsServiceEntity>(smsService);
            model.CreatedDate = DateTime.UtcNow;
            model.UpdatedDate = DateTime.UtcNow;
            model.Code = General.GenerateUniqueCode("3");
            model.DailyLimit = General.GenerateRandomNumber();
            model.IsActive = true;

            await _smsServiceRepository.AddAsync(model);
            //var id = model.Id;
            var resmodel = _mapper.Map<SmsServiceGetResponseDTO>(model);
            return resmodel;
        }

        public async Task<SmsServiceGetResponseDTO> GetAsync(long id)
        {
            var model = await _smsServiceRepository.GetAsync(id);
            if (model == null)
                return null;
            var viewModel = _mapper.Map<SmsServiceGetResponseDTO>(model);
            return viewModel;
        }

        public async Task<SmsServiceGetResponseDTO> GetAsync(string Code)
        {
            var model = await _smsServiceRepository.GetAsync(Code);
            if (model == null)
                return null;
            var viewModel = _mapper.Map<SmsServiceGetResponseDTO>(model);
            return viewModel;
        }
        public async Task<SmsThirdPartyResponseDTO> SendBulkSms(SendBulkSmsRequestDTO request, SmsServiceGetResponseDTO service)
        {
            SmsThirdPartyResponseDTO response =General.Cast(
                await General.CallServiceClient(request),typeof(SmsThirdPartyResponseDTO));
            return response;

        }

    }
}
