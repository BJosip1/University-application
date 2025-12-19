using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<ProgramType> ProgramTypes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
