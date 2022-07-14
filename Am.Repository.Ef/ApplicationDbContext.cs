using Am.Infrastructure.Entities;
using Am.Repository.Ef.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Am.Repository.Ef
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

        public DbSet<WeatherForecast> WeatherForecast { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new WeatherForcastEntityTypeConfiguration());
        }
    }
}
