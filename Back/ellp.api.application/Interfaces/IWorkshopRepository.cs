using Ellp.Api.Domain.Entities;

namespace Ellp.Api.Application.Interfaces
{
    public interface IWorkshopRepository
    {
        Task AddAsync(Workshop workshop);
    }
}
