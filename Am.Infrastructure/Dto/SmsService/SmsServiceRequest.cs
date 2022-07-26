using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Am.Infrastructure.Dto.SmsService
{
    public class SmsServiceCreateDTO
    {
        public string Name { get; set; }
    }

    public class SMSServiceGetDTO
    {
        public long Id { get; set; }
    }

    public class SendBulkSmsRequestDTO
    {
        public string Body { get; set; } = string.Empty;
        public List<string> PhoneNumbers { get; set; } = new List<string>();
    }

    public class SMSTransactionCreationDTO
    {
        public string ServiceCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }

}
