using Am.Infrastructure.Dto.SmsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Am.Infrastructure.IServices
{
    public interface ISmsService
    {
        Task<SmsServiceGetResponseDTO> GetAsync(string Code);
        Task<SmsServiceGetResponseDTO> GetAsync(long id);
        Task<SmsServiceGetResponseDTO> CreateSmsService(SmsServiceCreateDTO smsService);
    }
}
