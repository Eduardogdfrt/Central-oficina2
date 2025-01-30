
using Ellp.Api.Application.Interfaces;
using Ellp.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ellp.Api.Infra.SqlServer.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SqlServerDbContext _context;

        public StudentRepository(SqlServerDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task<Student> GetStudentByEmailAsync(string email)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.Email == email);
        }

        public async Task<Student> GetStudentByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.Students
                .FirstOrDefaultAsync(s => s.Email == email && s.Password == password);
        }
        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);   
        }
    }
}
