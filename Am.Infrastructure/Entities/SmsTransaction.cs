using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Am.Infrastructure.Entities
{
    public class SmsTransaction :  BaseEntity
    {
        public long ServiceId { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }

    }
}
