using System;
using System.Collections.Generic;

namespace Ellp.Api.Domain.Entities
{
    public class Professor
    {
        public Professor(int professorId, string name, string specialty, string password, string email)
        {
            ProfessorId = professorId;
            Name = name;
            Specialty = specialty;
            Password = password;
            Email = email;
        }
        public int ProfessorId { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}




