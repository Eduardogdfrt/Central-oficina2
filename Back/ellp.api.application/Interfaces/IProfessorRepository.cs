using Ellp.Api.Domain.Entities;
using System.Threading.Tasks;

namespace Ellp.Api.Application.Interfaces
{
    public interface IProfessorRepository : IRepository<Professor>
    {
        Task<Professor> GetAllProfessorInfosAsync(int professorId, string password);
        Task AddNewProfessorAsync(Professor professor);
        Task<Professor> GetByEmailAsync(string email);
    }
}

