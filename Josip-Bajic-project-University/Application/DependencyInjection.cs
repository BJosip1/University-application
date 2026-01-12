using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Repositories;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(assembly));

            services.AddScoped<IProgramTypeRepository, ProgramTypeRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IProfessorRepository, ProfessorRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();

            services.AddScoped<IProgramTypeService, ProgramTypeService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IProfessorService, ProfessorService>();
            services.AddScoped<ICourseService, CourseService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
