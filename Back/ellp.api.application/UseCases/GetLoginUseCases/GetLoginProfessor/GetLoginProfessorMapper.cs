using Ellp.Api.Domain.Entities;

namespace Ellp.Api.Application.UseCases.GetLoginUseCases.GetLoginProfessor
{
    public class GetLoginProfessorMapper
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int ProfessorId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public static GetLoginProfessorMapper ToLoginOutput(Professor professor)
        {
            return new GetLoginProfessorMapper
            {
                Success = true,
                Message = "Login successful",
                ProfessorId = professor.ProfessorId,
                Email = professor.Email,
                Name = professor.Name
            };
        }
    }
}
