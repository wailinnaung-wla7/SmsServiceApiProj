using Am.Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Am.Repository.Ef.Repository
{



    public class HelloWorldRepository : IHelloWorldRepository
    {
        private readonly ApplicationDbContext _context;
        public HelloWorldRepository(ApplicationDbContext  context) {
            _context = context;
        }

        public async Task<string> GetMessage()
        {
            return await _context.HelloWorld.Where(x => x.IsActive == true)
                .Select(x => x.Message).SingleAsync();
        }
    }
}
