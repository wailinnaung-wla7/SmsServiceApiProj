using Am.Infrastructure.Dto.SmsService;
using Am.Infrastructure.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Am.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SmsServiceController : ControllerBase
    {
        #region Private
        private readonly ISmsService _smsService;
        private readonly ILogger<SmsServiceController> _logger;
        #endregion

        // TODO: Add Centralize Logging
        public SmsServiceController(ISmsService smsService,
            ILogger<SmsServiceController> logger)
        {
            _smsService = smsService;
            _logger = logger;
        }
        [HttpGet("{id}", Name = "GetSmsService")]
        public async Task<ActionResult<SmsServiceGetResponseDTO>> GetSmsService(long id)
        {
            var viewModel = await _smsService.GetAsync(id);
            if (viewModel == null)
                return NotFound();
            return Ok(viewModel);
        }
        [HttpPost]
        public async Task<ActionResult> AddSmsService(SmsServiceCreateDTO smsService)
        {
            var model = await _smsService.CreateSmsService(smsService);
            //return CreatedAtRoute("GetSmsService", new { id = Id });
            return Ok(model);

        }

    }
}
