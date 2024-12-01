using Ellp.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Ellp.Api.Application.Interfaces;

namespace Ellp.Api.Infra.SqlServer.Repository
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(SqlServerDbContext context) : base(context)
        {
        }

        public async Task<Student> GetStudentByEmailAsync(string email)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.Email == email);
        }

        public async Task<Student> GetStudentByEmailAndPasswordAsync(string email, string password)
        {

          var a  = await _context.Students.FirstOrDefaultAsync(s => s.Email == email && s.Password == password);
            Console.WriteLine(a);
            return a;
        }
    }
}
