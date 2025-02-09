using Ellp.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Ellp.Api.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ellp.Api.Infra.SqlServer.Repository
{
    public class ProfessorRepository : Repository<Professor>, IProfessorRepository
    {
        public ProfessorRepository(SqlServerDbContext context) : base(context)
        {
        }

        public async Task<Professor> GetAllProfessorInfosAsync(int professorId, string password)
        {
            return await _context.Professors.FirstOrDefaultAsync(p => p.ProfessorId == professorId && p.Password == password);
        }

        public async Task AddNewProfessorAsync(Professor professor)
        {
            await _context.Professors.AddAsync(professor);
            await _context.SaveChangesAsync();
        }

        public async Task<Professor> GetByEmailAsync(string email)
        {
            return await _context.Professors.FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<Professor> GetProfessorByIdAsync(int id)
        {
            return await _context.Professors.FindAsync(id);
        }

        public async Task<IEnumerable<Professor>> GetAllAsync()
        {
            return await _context.Professors.ToListAsync();
        }
    }
}

