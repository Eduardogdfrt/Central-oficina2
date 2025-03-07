﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Ellp.Api.Domain.Entities;

namespace Ellp.Api.Application.Interfaces
{
    public interface IStudentRepository
    {
        Task AddAsync(Student student);
        Task<Student> GetStudentByEmailAsync(string email);
        Task<Student> GetStudentByEmailAndPasswordAsync(string email, string password);
        Task<Student> GetStudentByIdAsync(int id);
        Task<IEnumerable<Student>> GetAllAsync(); 
    }
}
