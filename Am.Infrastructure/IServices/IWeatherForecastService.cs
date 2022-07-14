using Am.Infrastructure.Dto.WeatherForecast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Am.Infrastructure.IServices
{
    public interface IWeatherForecastService
    {
        Task<List<WeatherForecastResponseViewModel>> GetAllAsync();
        List<WeatherForecastResponseViewModel> GetAll();
        Task<WeatherForecastResponseViewModel> GetAsync(long id);
        Task<bool> AddAsync(WeatherForecastRequestViewModel model);
        Task<bool> UpdateAsync(WeatherForecastRequestViewModel model);
        Task<bool> DeleteAsync(long id);
    }
}
