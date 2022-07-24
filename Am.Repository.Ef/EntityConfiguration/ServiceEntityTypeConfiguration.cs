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
    class ServiceEntityTypeConfiguration : IEntityTypeConfiguration<SmsServiceEntity>
    {
        public void Configure(EntityTypeBuilder<SmsServiceEntity> builder)
        {
            // TODO: Define Table Schema Details
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code).HasMaxLength(50);
            builder.Property(x => x.Name).HasMaxLength(100);

            // Seed Data

            builder.HasData(
            new SmsServiceEntity
            {
                Id = 1,
                Name = "Test Service",
                Code = "1" + DateTime.UtcNow.ToString("yyyyMMddHHmmss"),
                DailyLimit = 40,
                CreatedDate = DateTime.UtcNow
            });
        }
    }
}
