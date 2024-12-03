

namespace Ellp.Api.Domain.Entities
{
    public class Student
    {
        public Student(int id, string name, string email, string password, DateTime BirthDate, bool IsAuthenticated)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            this.BirthDate = BirthDate;
            this.IsAuthenticated = IsAuthenticated;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public DateTime BirthDate { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
