using Ellp.Api.Domain.Entities;

namespace Ellp.Api.Application.UseCases.Users.AddParticipantUsecases.AddNewProfessorUseCases
{
    public class AddNewProfessorOutput
    {
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
