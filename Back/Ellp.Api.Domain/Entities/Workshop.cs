using System;
using System.Collections.Generic;

namespace Ellp.Api.Domain.Entities
{
    public class Workshop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProfessorIdW { get; set; }
        public int? HelperIDW { get; set; }
        public DateTime Data { get; set; }

        public Professor Professor { get; set; }
        public Student Helper { get; set; } 
        public ICollection<WorkshopAluno> WorkshopAlunos { get; set; } = new List<WorkshopAluno>();
    }
}

