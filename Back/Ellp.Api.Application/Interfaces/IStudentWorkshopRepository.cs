using Ellp.Api.Domain.Entities;

public interface IStudentWorkshopRepository
{
    Task AddStudentWorkshopAsync(int studentId, int workshopId);
    Task<WorkshopAluno?> GetStudentWorkshopAsync(int studentId, int workshopId);
    Task DeleteStudentWorkshopAsync(WorkshopAluno studentWorkshop);
    Task<List<Workshop>> GetAllWorkshopsByStudentIdAsync(int studentId);
    Task<List<Student>> GetAllStudentsByWorkshopIdAsync(int workshopId);
    Task EmitirCertificadoAsync(int studentId, int workshopId, string certificado);
}


