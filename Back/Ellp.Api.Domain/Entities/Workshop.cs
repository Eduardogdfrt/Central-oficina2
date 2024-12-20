using System;

namespace Ellp.Api.Domain.Entities
{
    public class Workshop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProfessorIdW { get; set; }
        public int? HelperIDW { get; set; } 
        public DateTime Data { get; set; }

        public Student Student { get; set; }
        public Professor Professor { get; set; }
    }
}
