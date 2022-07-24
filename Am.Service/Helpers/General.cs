using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Am.Service.Helpers
{
    public static class General
    {
        public static string GenerateUniqueCode(string Id)
        {
            return Id + DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        }
        public static int GenerateRandomNumber()
        {
            Random rnd = new Random();
            int random = rnd.Next(40, 101);
            return random;
        }
    }
}
