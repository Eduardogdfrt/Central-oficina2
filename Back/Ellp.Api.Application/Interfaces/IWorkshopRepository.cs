using Ellp.Api.Domain.Entities;

namespace Ellp.Api.Application.Interfaces
{
    public interface IWorkshopRepository
    {
        Task AddAsync(Workshop workshop);
        Task<Workshop> GetWorkshopByIdAsync(int id);
        Task<List<Workshop>> GetWorkshopAllAsync();
        Task<List<Workshop>> GetWorkshopAllforProfessorsAsync(int professorId);

    }
}
