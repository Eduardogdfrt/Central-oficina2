using Ellp.Api.Domain.Entities;

namespace Ellp.Api.Application.UseCases.Users.GetLoginUseCases.GetLoginStudent
{
    public class GetLoginStudentOutput
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StudentId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public static GetLoginStudentOutput ToLoginOutput(Student student)
        {
            return new GetLoginStudentOutput
            {
                Success = true,
                Message = "Login successful",
                StudentId = student.Id,
                Email = student.Email,
                Name = student.Name
            };
        }
        public static GetLoginStudentOutput ToLoginOutputIfInvalid(Student student)
        {
            return new GetLoginStudentOutput
            {
                Success = false,
                Message = "Login Failed",
            };
        }
    }
}




