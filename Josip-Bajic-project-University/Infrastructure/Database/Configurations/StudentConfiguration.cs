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
    public class StudentConfiguration: IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .IsRequired();

            builder.Property(s => s.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(s => s.Surname)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(s => s.Email)
                .HasMaxLength(150)
                .IsRequired();

            builder.HasIndex(s => s.Email)
                .IsUnique();
                
            builder.Property(s => s.Major)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(s => s.BirthDate)
                .IsRequired(false);

            builder.Property(s => s.EnrollmentDate)
                .IsRequired();

            builder.Property(s => s.IsActive)
                .IsRequired();

            builder.HasOne(s=>s.User)
                .WithOne(u => u.Student)
                .HasForeignKey<Student>(s=>s.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(s => s.ProgramType)
                .WithMany(p => p.Students)
                .HasForeignKey(s => s.ProgramTypeId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(s => s.EnrolledCourses)
                .WithMany(c => c.Students)
            .UsingEntity<StudentCourse>(
                sc => sc.HasOne<Course>()
                      .WithMany()
                      .HasForeignKey(x => x.EnrolledCoursesId)
                      .OnDelete(DeleteBehavior.Cascade),
                sc => sc.HasOne<Student>()
                      .WithMany()
                      .HasForeignKey(x => x.StudentsId)
                      .OnDelete(DeleteBehavior.Cascade),
                sc =>
                {
                    sc.ToTable("StudentsCourses");
                    sc.HasKey(x => new { x.StudentsId, x.EnrolledCoursesId });
                }
            );
        }
    }
    
}
