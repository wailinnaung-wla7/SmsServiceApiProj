using Am.Infrastructure.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Am.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IHelloService _service;
        public HomeController(IHelloService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            return await _service.Test();
        }
    }

   
}
