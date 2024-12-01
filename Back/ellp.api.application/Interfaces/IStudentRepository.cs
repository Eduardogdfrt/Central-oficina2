using Ellp.Api.Application.Interfaces;
using Ellp.Api.Domain.Entities;

public interface IStudentRepository : IRepository<Student>
{
    Task<Student> GetStudentByEmailAsync(string email);
    Task<Student> GetStudentByEmailAndPasswordAsync(string email, string password);
}




