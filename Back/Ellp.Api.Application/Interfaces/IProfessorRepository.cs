using Ellp.Api.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ellp.Api.Application.Interfaces
{
    public interface IProfessorRepository : IRepository<Professor>
    {
        Task<Professor> GetAllProfessorInfosAsync(int professorId, string password);
        Task AddNewProfessorAsync(Professor professor);
        Task<Professor> GetByEmailAsync(string email);
        Task<Professor> GetProfessorByIdAsync(int id); // Adicionado
        Task<IEnumerable<Professor>> GetAllAsync(); // Adicionado
    }
}

