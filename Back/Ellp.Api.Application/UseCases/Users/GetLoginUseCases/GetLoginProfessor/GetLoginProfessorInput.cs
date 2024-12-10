using MediatR;

namespace Ellp.Api.Application.UseCases.Users.GetLoginUseCases.GetLoginProfessor
{
    public class GetLoginProfessorInput : IRequest<GetLoginProfessorMapper>
    {
        public int ProfessorId { get; set; }
        public string? Password { get; set; }
    }
}
