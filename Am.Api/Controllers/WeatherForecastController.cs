using Am.Infrastructure.Dto.WeatherForecast;
using Am.Infrastructure.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Am.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        #region Private
        private readonly IWeatherForecastService _weatherForecastService;
        private readonly ILogger<WeatherForecastController> _logger;
        #endregion

        // TODO: Add Centralize Logging
        public WeatherForecastController(IWeatherForecastService weatherForecastService,
            ILogger<WeatherForecastController> logger)
        {
            _weatherForecastService = weatherForecastService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<WeatherForecastResponseViewModel>>> GetWeatherForecasts()
        {
            return await _weatherForecastService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherForecastResponseViewModel>> GetWeatherForecast(long id)
        {
            var viewModel = await _weatherForecastService.GetAsync(id);
            if(viewModel == null)
                return NotFound();
            return viewModel;
        }

        [HttpPost]
        public async Task<ActionResult> AddWeatherForecast(WeatherForecastRequestViewModel viewModel)
        {
            await _weatherForecastService.AddAsync(viewModel);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWeatherForecast(long id, WeatherForecastRequestViewModel viewModel)
        {
            if (id != viewModel.Id)
                return BadRequest();

            await _weatherForecastService.UpdateAsync(viewModel);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            //throw new ArgumentNullException("Exception from API");
            await _weatherForecastService.DeleteAsync(id);
            return NoContent();
        }
    }
}