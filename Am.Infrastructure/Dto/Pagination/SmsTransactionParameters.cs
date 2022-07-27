using Am.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Am.Infrastructure.Dto.Pagination
{
    public class SmsTransactionParameters : PaginationAbstractClass
    {
        public string ServiceCode { get; set; } = string.Empty;
    }
}
