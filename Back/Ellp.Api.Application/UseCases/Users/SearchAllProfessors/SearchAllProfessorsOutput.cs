using System;
using System.Collections.Generic;
using Ellp.Api.Domain.Entities;

namespace Ellp.Api.Application.UseCases.Users.SearchAllProfessors
{
    public class SearchAllProfessorsOutput
    {
        public int ProfessorId { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public string Email { get; set; }

        public SearchAllProfessorsOutput(int professorId, string name, string specialty, string email)
        {
            ProfessorId = professorId;
            Name = name;
            Specialty = specialty;
            Email = email;
        }

        public static SearchAllProfessorsOutput FromEntity(Professor professor)
        {
            return new SearchAllProfessorsOutput(
                professor.ProfessorId,
                professor.Name,
                professor.Specialty,
                professor.Email
            );
        }
    }
}
