using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Configurations
{
    public class ProgramTypeConfiguration:IEntityTypeConfiguration<ProgramType>
    {
        public void Configure(EntityTypeBuilder<ProgramType> builder)
        {
            builder.ToTable("ProgramTypes");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired();

            builder.Property(x=>x.Title)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
