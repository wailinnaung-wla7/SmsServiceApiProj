using Am.Infrastructure.Entities;
using Am.Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Am.Repository.Ef.Repository
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        #region private
        private readonly ApplicationDbContext _context;
        #endregion
        public WeatherForecastRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<WeatherForecast>> GetAllAsync()
        {
            return await _context.WeatherForecast.ToListAsync();
        }

        public List<WeatherForecast> GetAll()
        {
            return _context.WeatherForecast.ToList();
        }

        public async Task<WeatherForecast> GetAsync(long id)
        {
            return await _context.WeatherForecast.FindAsync(id);
        }

        public async Task<WeatherForecast> AddAsync(WeatherForecast model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<WeatherForecast> UpdateAsync(WeatherForecast model)
        {
            _context.WeatherForecast.Attach(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var model = await _context.WeatherForecast.FindAsync(id);
            if (model == null)
                return false;

            _context.Remove(model);
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
