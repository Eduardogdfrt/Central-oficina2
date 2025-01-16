using System.Threading.Tasks;
using Ellp.Api.Application.Interfaces;
using Ellp.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ellp.Api.Infra.SqlServer.Repository
{
    public class StudentWorkshopRepository : IStudentWorkshopRepository
    {
        private readonly SqlServerDbContext _context;

        public StudentWorkshopRepository(SqlServerDbContext context)
        {
            _context = context;
        }

        public async Task AddStudentWorkshopAsync(int studentId, int workshopId)
        {
            var studentWorkshop = new WorkshopAluno
            {
                StudentId = studentId,
                WorkshopId = workshopId
            };

            _context.WorkshopStudents.Add(studentWorkshop);
            await _context.SaveChangesAsync();
        }
    }
}
