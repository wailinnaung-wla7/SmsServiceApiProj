using Am.Infrastructure.Dto.WeatherForecast;
using Am.Infrastructure.Entities;
using Am.Infrastructure.IRepositories;
using Am.Infrastructure.IServices;
using AutoMapper;

namespace Am.Service.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        #region Private
        private readonly IMapper _mapper;
        private readonly IWeatherForecastRepository _weatherForecastRepository;
        #endregion
        public WeatherForecastService(IWeatherForecastRepository weatherForecastRepository,
            IMapper mapper)
        {

            _weatherForecastRepository = weatherForecastRepository;
            _mapper = mapper;
        }

        public List<WeatherForecastResponseViewModel> GetAll()
        {
            //throw new ArgumentNullException("Dummy Exception Message");
            //throw new AppException("From Application Exception");
            var models = _weatherForecastRepository.GetAll();
            var viewModels = _mapper.Map<List<WeatherForecastResponseViewModel>>(models);
            return viewModels;
        }

        public async Task<List<WeatherForecastResponseViewModel>> GetAllAsync()
        {
            var models = await _weatherForecastRepository.GetAllAsync();
            var viewModels = _mapper.Map<List<WeatherForecastResponseViewModel>>(models);
            return viewModels;
        }

        public async Task<WeatherForecastResponseViewModel> GetAsync(long id)
        {
            var model = await _weatherForecastRepository.GetAsync(id);
            if (model == null)
                return null;
            var viewModel = _mapper.Map<WeatherForecastResponseViewModel>(model);
            return viewModel;
        }

        public async Task<bool> UpdateAsync(WeatherForecastRequestViewModel model)
        {
            var m = await _weatherForecastRepository.GetAsync(model.Id);
            if (m == null)
                return false;
            m.TemperatureF = model.TemperatureF;
            m.TemperatureC = model.TemperatureC;
            m.UpdatedBy = model.UpdatedBy;
            m.Summary = model.Summary;
            m.IsActive = model.IsActive;
            m.UpdatedDate = DateTime.UtcNow;

            await _weatherForecastRepository.UpdateAsync(m);
            return true;
        }

        public async Task<bool> AddAsync(WeatherForecastRequestViewModel viewModel)
        {
            var model = _mapper.Map<WeatherForecast>(viewModel);
            model.CreatedDate = DateTime.UtcNow;
            model.UpdatedDate = DateTime.UtcNow;
            model.IsActive = true;

            await _weatherForecastRepository.AddAsync(model);
            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            //throw new ArgumentNullException("Exception from Service");
            return await _weatherForecastRepository.DeleteAsync(id);
        }
    }
}
