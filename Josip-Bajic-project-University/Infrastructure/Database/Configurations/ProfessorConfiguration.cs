using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Configurations
{
    public class ProfessorConfiguration: IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder) 
        {
            builder.ToTable("Professors");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .IsRequired();

            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p=>p.Surname)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Email)
                .HasMaxLength(150)
                .IsRequired();

            builder.HasIndex(p => p.Email)
                .IsUnique();

            builder.Property(p => p.Department)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(p => p.HireDate)
                .IsRequired(false);

            builder.HasOne(p => p.User)
                .WithOne(u => u.Professor)
                .HasForeignKey<Professor>(p => p.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            builder.HasMany(p => p.TeachingCourses)
                .WithMany(c => c.Professors)
                .UsingEntity(j => j.ToTable("ProfessorCourses"));
        }
    }
}
