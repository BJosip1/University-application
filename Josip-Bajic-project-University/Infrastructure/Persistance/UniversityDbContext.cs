using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistance
{
    public class UniversityDbContext:DbContext, IApplicationDbContext
    {
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
            : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<ProgramType> ProgramTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProfessorCourse> ProfessorCourses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgramType>().HasData(
                new ProgramType { Id = 1, Title=  "Svučilišni prijediplomski studij" },
                new ProgramType { Id = 2, Title = "Stručni prijediplomski studij" },
                new ProgramType { Id = 3, Title = "Diplomski sveučilišni studij" },
                new ProgramType { Id = 4, Title = "Diplomski stručni studij" },
                new ProgramType { Id = 5, Title = "Doktorski studij" },
                new ProgramType { Id = 6, Title = "Razlikovni studij" }
                );
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UniversityDbContext).Assembly);
        }
    }
}
