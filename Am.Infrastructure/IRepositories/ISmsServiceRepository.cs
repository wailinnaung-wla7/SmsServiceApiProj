using Am.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Am.Infrastructure.IRepositories
{
    public interface ISmsServiceRepository : IRepository<SmsServiceEntity>
    {
        Task<SmsServiceEntity> GetAsync(string Code);
        Task<SmsServiceEntity> GetAsync(long id);
        Task<SmsServiceEntity> AddAsync(SmsServiceEntity model);
    }
}
