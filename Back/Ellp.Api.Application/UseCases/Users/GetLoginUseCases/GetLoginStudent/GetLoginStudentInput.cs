using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Ellp.Api.Application.UseCases.Users.GetLoginUseCases.GetLoginStudent
{
    public class GetLoginStudentInput : IRequest<GetLoginStudentOutput>
    {
        [Required]
        public string Email { get; set; }
        public string? Password { get; set; } // Permitir valores nulos
    }
}
