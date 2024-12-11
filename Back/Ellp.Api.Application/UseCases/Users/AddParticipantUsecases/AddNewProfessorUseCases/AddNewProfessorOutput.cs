using Ellp.Api.Domain.Entities;

namespace Ellp.Api.Application.UseCases.Users.AddParticipantUsecases.AddNewProfessorUseCases
{
    public class AddNewProfessorOutput
    {
        public int ProfessorId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Message { get; set; }

        public static AddNewProfessorInput ToInput(Professor professor, string password)
        {
            return new AddNewProfessorInput
            {
                ProfessorId = professor.ProfessorId,
                Name = professor.Name,
                Specialty = professor.Specialty,
                Password = password,
                Email = professor.Email
            };
        }
    }
}

