using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Ellp.Api.Application.UseCases.GetLoginUseCases.GetLoginStudent
{
    public class GetLoginStudentInput : IRequest<GetLoginStudentMapper>
    {
        [Required]
        public string Email { get; set; }
        public string? Password { get; set; } // Permitir valores nulos
    }
}
