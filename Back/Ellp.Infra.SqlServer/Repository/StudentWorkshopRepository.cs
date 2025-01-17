using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ellp.Api.Application.Interfaces;
using Ellp.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ellp.Api.Infra.SqlServer.Repository
{
    public class StudentWorkshopRepository : IStudentWorkshopRepository
    {
        private readonly SqlServerDbContext _context;
        private readonly ILogger<StudentWorkshopRepository> _logger;

        public StudentWorkshopRepository(SqlServerDbContext context, ILogger<StudentWorkshopRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddStudentWorkshopAsync(int studentId, int workshopId)
        {
            var studentWorkshop = new WorkshopAluno
            {
                StudentId = studentId,
                WorkshopId = workshopId
            };

            await _context.WorkshopStudents.AddAsync(studentWorkshop);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao salvar as alterações no banco de dados.");
                throw;
            }
        }

        public async Task<WorkshopAluno> GetStudentWorkshopAsync(int studentId, int workshopId)
        {
            return await _context.WorkshopStudents
                .FirstOrDefaultAsync(wa => wa.StudentId == studentId && wa.WorkshopId == workshopId);
        }

        public async Task DeleteStudentWorkshopAsync(WorkshopAluno studentWorkshop)
        {
            _context.WorkshopStudents.Remove(studentWorkshop);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Workshop>> GetAllWorkshopsByStudentIdAsync(int studentId)
        {
            return await _context.WorkshopStudents
                .Where(wa => wa.StudentId == studentId)
                .Select(wa => wa.Workshop)
                .ToListAsync();
        }

        public async Task<List<Student>> GetAllStudentsByWorkshopIdAsync(int workshopId)
        {
            return await _context.WorkshopStudents
                .Where(wa => wa.WorkshopId == workshopId)
                .Select(wa => wa.Student)
                .ToListAsync();
        }

        public async Task EmitirCertificadoAsync(int studentId, int workshopId, string certificado)
        {
            var studentWorkshop = await GetStudentWorkshopAsync(studentId, workshopId);
            if (studentWorkshop != null)
            {
                studentWorkshop.Certificate = certificado;
                await _context.SaveChangesAsync();
            }
        }
    }
}



