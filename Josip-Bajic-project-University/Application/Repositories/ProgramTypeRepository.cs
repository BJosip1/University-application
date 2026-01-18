using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public class ProgramTypeRepository:IProgramTypeRepository
    {
            private readonly IApplicationDbContext _dbContext;

            public ProgramTypeRepository(IApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<IEnumerable<ProgramType>> GetAllProgramTypes()
            {
                return await _dbContext.ProgramTypes
                    .AsNoTracking()
                    .ToListAsync();
            }
        }
    }
