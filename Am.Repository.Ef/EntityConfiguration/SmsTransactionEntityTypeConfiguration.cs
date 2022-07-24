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
    public class SmsTransactionEntityTypeConfiguration : IEntityTypeConfiguration<SmsTransaction>
    {
        public void Configure(EntityTypeBuilder<SmsTransaction> builder)
        {
            // TODO: Define Table Schema Details
            builder.HasKey(x => x.Id);
            // Seed Data

            builder.HasData(
            new SmsTransaction
            {
                Id = 1,
                ServiceId = 1,
                PhoneNumber =  "09795831832",
                Message = "Hi there!",
                CreatedDate = DateTime.UtcNow
            });
        }
    }
    
}
