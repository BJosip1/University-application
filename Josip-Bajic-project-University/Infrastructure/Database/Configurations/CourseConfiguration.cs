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
    public class CourseConfiguration:IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");

            builder.HasKey(c=>c.Id);
            builder.Property(c => c.Id)
                .IsRequired();

            builder.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c=>c.CourseCode)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(c => c.Description)
                .HasMaxLength(1000)
                .IsRequired(false);

        }
    }
}
