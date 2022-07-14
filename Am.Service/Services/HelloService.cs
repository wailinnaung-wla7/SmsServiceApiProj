using Am.Infrastructure.IRepositories;
using Am.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Am.Service.Services
{
    public class HelloService : IHelloService
    {
        private readonly IHelloWorldRepository _repo;
        public HelloService(IHelloWorldRepository repo)
        {
            _repo = repo;
        }
        public async Task<string> Test()
        {
            //throw new NotImplementedException();
            return await _repo.GetMessage();
        }
    }
}
