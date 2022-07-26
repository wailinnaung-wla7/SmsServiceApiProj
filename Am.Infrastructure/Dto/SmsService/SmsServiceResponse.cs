using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Am.Infrastructure.Dto.SmsService
{
    public class SmsServiceGetResponseDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int DailyLimit { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class SendBulkSmsResponseDTO
    {
        public string OverallStatus { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public int DailyLimitLeftForToday { get; set; }
        public List<string> FailedToSendNumbers { get; set; } = new List<string>();
    }

    public class SmsThirdPartyResponseDTO
    {
        public List<string> FailedToSendNumbers { get; set; } = new List<string>();
    }
}
