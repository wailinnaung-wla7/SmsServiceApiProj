using Am.Api.Helpers;
using Am.Infrastructure.Dto.Pagination;
using Am.Infrastructure.Dto.SmsService;
using Am.Infrastructure.Entities;
using Am.Infrastructure.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Am.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SmsServiceController : ControllerBase
    {
        #region Private
        private readonly ISmsService _smsService;
        private readonly ISmsTransactionService _smsTransactionService;
        private readonly ILogger<SmsServiceController> _logger;
        #endregion

        // TODO: Add Centralize Logging
        public SmsServiceController(ISmsService smsService, ISmsTransactionService smsTransactionService,
            ILogger<SmsServiceController> logger)
        {
            _smsService = smsService;
            _smsTransactionService = smsTransactionService;
            _logger = logger;
        }
        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetSmsService")]
        public async Task<ActionResult<SmsServiceGetResponseDTO>> GetSmsService(long id)
        {
            var viewModel = await _smsService.GetAsync(id);
            if (viewModel == null)
                return NotFound();
            return Ok(viewModel);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> AddSmsService(SmsServiceCreateDTO smsService)
        {
            var model = await _smsService.CreateSmsService(smsService);
            //return CreatedAtRoute("GetSmsService", new { id = Id });
            return Ok(model);

        }
        [HttpPost("SendBulkSms")]
        public async Task<IActionResult> SendBulkSms(SendBulkSmsRequestDTO request)
        {
            var ServiceCode = JwtHelper.GetServiceCodeFromJwtToken(HttpContext);
            var service = await _smsService.GetAsync(ServiceCode);
            if (service == null)
            {
                return BadRequest("Service Not Found");
            }
            var SmsUsageForToday = await _smsTransactionService.GetSmsTransactionsForToday(ServiceCode);
            if (SmsUsageForToday + request.PhoneNumbers.Count > service.DailyLimit)
            {
                return BadRequest($"Only {service.DailyLimit - SmsUsageForToday} Sms Left For {service.Name} Today");
            }
            //MockThirdPartySmsProvider
            var thirdPartyResponseDTO = await _smsService.SendBulkSms(request, service);

            //UpdateSmsTransactionsStatusInDb
            var model = await _smsTransactionService.CreateSMSTransactions(request, thirdPartyResponseDTO.FailedToSendNumbers, ServiceCode);
            return Ok(new SendBulkSmsResponseDTO
            {
                OverallStatus = "Success",
                ServiceName = service.Name,
                FailedToSendNumbers = thirdPartyResponseDTO.FailedToSendNumbers,
                Code = service.Code,
                DailyLimitLeftForToday = service.DailyLimit - (SmsUsageForToday + (
                request.PhoneNumbers.Count-thirdPartyResponseDTO.FailedToSendNumbers.Count()
                ))
            });
        }
        [AllowAnonymous]
        [HttpGet("Sms-Transaction")]
        public async Task<IActionResult> GetSmsTransaction([FromQuery]SmsTransactionParameters parameters)
        {
            var trans = await _smsTransactionService.GetSmsTransactions(parameters);
            var metadata = new
            {
                trans.TotalCount,
                trans.PageSize,
                trans.CurrentPage,
                trans.TotalPages,
                trans.HasNext,
                trans.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(trans);
        }

        //ForMockSMSProvider
        [AllowAnonymous]
        [HttpPost("SendSmsFromThirdParty")]
        public async Task<IActionResult> SendSmsFromThirdParty(SendBulkSmsRequestDTO request)
        {
            var response = new SmsThirdPartyResponseDTO();
            var rnd = new Random();
            response.FailedToSendNumbers = request.PhoneNumbers.OrderBy(
                x => rnd.Next()).Take(
                new Random().Next(
                    0, request.PhoneNumbers.Count())).ToList();
            return Ok(response);
        }
    }
}
