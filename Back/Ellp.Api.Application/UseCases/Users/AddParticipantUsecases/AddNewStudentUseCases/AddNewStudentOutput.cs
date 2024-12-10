using Ellp.Api.Domain.Entities;
using Ellp.Api.Application.Utilities;

namespace Ellp.Api.Application.UseCases.Users.AddParticipantUsecases.AddNewStudentUseCases
{
    public static class AddNewStudentOutput
    {
        public static Student ToEntity(AddNewStudentInput input)
        {
            return new Student(
                id: 0,
                name: input.Name,
                email: input.Email,
                password: input.Password, 
                BirthDate: input.BirthDate,
                IsAuthenticated: false 
            );
        }
    }
}
