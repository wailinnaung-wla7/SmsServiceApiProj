using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Am.Infrastructure.Entities
{
    public class SmsServiceEntity : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int DailyLimit { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
