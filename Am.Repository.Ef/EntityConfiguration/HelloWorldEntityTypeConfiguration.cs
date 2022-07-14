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
    public class HelloWorldEntityTypeConfiguration : IEntityTypeConfiguration<HelloWorld>
    {
        public void Configure(EntityTypeBuilder<HelloWorld> builder)
        {
            builder.HasData(new HelloWorld
            {
                Id = 1,
                Message = "Hello World Message",
                CreatedBy = "minmhan",
                UpdatedBy = "minmhan",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                IsActive     = true
            });
        }
    } 
}
