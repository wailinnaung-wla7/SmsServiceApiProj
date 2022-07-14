using Am.Infrastructure.IRepositories;
using Am.Infrastructure.IServices;
using Am.Repository.Ef.Repository;
using Am.Service.Services;

namespace Am.Api.Extensions
{
    public static class AppExtensions
    {
        public static IServiceCollection AddConfig(this IServiceCollection services)
        {
            #region Repository
            services.AddTransient<IWeatherForecastRepository, WeatherForecastRepository>();
            #endregion

            #region Service
            services.AddTransient<IWeatherForecastService, WeatherForecastService>();
            #endregion           

            return services;
        }
    }
}
