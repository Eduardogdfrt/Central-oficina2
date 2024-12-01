using System;

namespace Ellp.Api.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // Permitir valores nulos
        public DateTime BirthDate { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
