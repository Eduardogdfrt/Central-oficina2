using Ellp.Api.Domain.Entities;

namespace Ellp.Api.Application.UseCases.GetLoginUseCases.GetLoginStudent
{
    public class GetLoginStudentMapper
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StudentId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public static GetLoginStudentMapper ToLoginOutput(Student student)
        {
            return new GetLoginStudentMapper
            {
                Success = true,
                Message = "Login successful",
                StudentId = student.Id,
                Email = student.Email,
                Name = student.Name
            };
        }
        public static GetLoginStudentMapper ToLoginOutputIfInvalid(Student student)
        {
            return new GetLoginStudentMapper
            {
                Success = false,
                Message = "Login Failed",
            };
        }
    }
}




