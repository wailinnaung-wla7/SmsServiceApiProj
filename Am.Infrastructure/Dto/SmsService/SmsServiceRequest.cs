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
}
