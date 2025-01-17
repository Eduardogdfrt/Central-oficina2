using System.Collections.Generic;
using System.Threading.Tasks;
using Ellp.Api.Domain.Entities;

namespace Ellp.Api.Application.Interfaces
{
    public interface IStudentWorkshopRepository
    {
        Task AddStudentWorkshopAsync(int studentId, int workshopId);
        Task<WorkshopAluno> GetStudentWorkshopAsync(int studentId, int workshopId);
        Task DeleteStudentWorkshopAsync(WorkshopAluno studentWorkshop);
        Task<List<Workshop>> GetAllWorkshopsByStudentIdAsync(int studentId);
        Task<List<Student>> GetAllStudentsByWorkshopIdAsync(int workshopId);
        Task EmitirCertificadoAsync(int studentId, int workshopId, string certificado);
    }
}


