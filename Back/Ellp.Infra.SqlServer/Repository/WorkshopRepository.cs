using System.Threading.Tasks;
using Ellp.Api.Application.Interfaces;
using Ellp.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ellp.Api.Infra.SqlServer.Repository
{
    public class WorkshopRepository : IWorkshopRepository
    {
        private readonly SqlServerDbContext _context;

        public WorkshopRepository(SqlServerDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Workshop workshop)
        {
            await _context.Set<Workshop>().AddAsync(workshop);
            await _context.SaveChangesAsync();
        }
    }
}
