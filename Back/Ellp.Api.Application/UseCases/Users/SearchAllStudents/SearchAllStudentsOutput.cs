using System;
using System.Collections.Generic;
using Ellp.Api.Domain.Entities;

namespace Ellp.Api.Application.UseCases.Users.SearchAllStudents
{
    public class SearchAllStudentsOutput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsAuthenticated { get; set; }
        public ICollection<WorkshopAluno> WorkshopAlunos { get; set; }

        public SearchAllStudentsOutput(int id, string name, string email, DateTime birthDate, bool isAuthenticated, ICollection<WorkshopAluno> workshopAlunos)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            IsAuthenticated = isAuthenticated;
            WorkshopAlunos = workshopAlunos;
        }

        public static SearchAllStudentsOutput FromEntity(Student student)
        {
            return new SearchAllStudentsOutput(
                student.Id,
                student.Name,
                student.Email,
                student.BirthDate,
                student.IsAuthenticated,
                student.WorkshopAlunos
            );
        }
    }
}

