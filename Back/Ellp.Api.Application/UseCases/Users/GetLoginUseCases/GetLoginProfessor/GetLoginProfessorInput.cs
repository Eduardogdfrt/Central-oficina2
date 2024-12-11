using MediatR;

namespace Ellp.Api.Application.UseCases.Users.GetLoginUseCases.GetLoginProfessor
{
    public class GetLoginProfessorInput : IRequest<GetLoginProfessorOutput>
    {
        public int ProfessorId { get; set; }
        public string? Password { get; set; }
    }
}
