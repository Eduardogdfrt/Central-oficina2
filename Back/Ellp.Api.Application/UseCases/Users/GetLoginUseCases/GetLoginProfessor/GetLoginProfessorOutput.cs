using Ellp.Api.Domain.Entities;

namespace Ellp.Api.Application.UseCases.Users.GetLoginUseCases.GetLoginProfessor
{
    public class GetLoginProfessorOutput
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int ProfessorId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public static GetLoginProfessorOutput ToLoginOutput(Professor professor)
        {
            return new GetLoginProfessorOutput
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
