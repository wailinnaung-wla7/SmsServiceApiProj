using Am.Infrastructure.Entities;

namespace Am.Infrastructure.IRepositories
{
    public interface IWeatherForecastRepository : IRepository<WeatherForecast>
    {
        Task<List<WeatherForecast>> GetAllAsync();

        List<WeatherForecast> GetAll();

        Task<WeatherForecast> GetAsync(long id);

        Task<WeatherForecast> AddAsync(WeatherForecast model);
        Task<WeatherForecast> UpdateAsync(WeatherForecast model);

        Task<bool> DeleteAsync(long id);
    }
}
