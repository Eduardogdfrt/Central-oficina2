using Ellp.Api.Domain.Entities;
using Ellp.Api.Application.Utilities;

namespace Ellp.Api.Application.UseCases.AddParticipantUsecases.AddNewStudentUseCases
{
    public static class AddNewStudentMapper
    {
        public static Student ToEntity(AddNewStudentInput input)
        {
            return new Student(
                id: 0, // Supondo que o ID será gerado automaticamente pelo banco de dados
                name: input.Name,
                email: input.Email,
                password: PasswordHasher.HashPassword(input.Password), // Hashear a senha recebida
                BirthDate: input.BirthDate,
                IsAuthenticated: false // Valor padrão
            );
        }
    }
}
