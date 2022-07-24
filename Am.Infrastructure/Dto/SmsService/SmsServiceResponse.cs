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
}
