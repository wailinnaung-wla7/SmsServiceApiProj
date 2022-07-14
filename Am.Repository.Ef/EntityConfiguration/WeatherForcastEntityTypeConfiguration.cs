using Am.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Am.Repository.Ef.EntityConfiguration
{
    class WeatherForcastEntityTypeConfiguration : IEntityTypeConfiguration<WeatherForecast>
    {
        public void Configure(EntityTypeBuilder<WeatherForecast> builder) {
            // TODO: Define Table Schema Details
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Summary).HasMaxLength(20);

            // Seed Data
            builder.HasData(
                new WeatherForecast
                {
                    Id = 1,
                    TemperatureC = 32,
                    TemperatureF = 98,
                    Date = DateTime.UtcNow,
                    CreatedBy = "minmhan",
                    UpdatedBy = "minmhan",
                    IsActive = true,
                    Summary = "Dummy Data",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                }) ;
        }
    }
}
