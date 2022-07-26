using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Am.Infrastructure.Entities
{
    public class SmsTransaction :  BaseEntity
    {
        public string ServiceCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public string Status { get; set; } = string.Empty;

    }
}
