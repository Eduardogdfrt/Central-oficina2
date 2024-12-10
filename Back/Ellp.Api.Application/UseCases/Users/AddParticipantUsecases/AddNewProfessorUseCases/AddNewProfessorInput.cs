using System.ComponentModel.DataAnnotations;
using MediatR;
using Ellp.Api.Application.Utilities;

namespace Ellp.Api.Application.UseCases.Users.AddParticipantUsecases.AddNewProfessorUseCases
{
    public class AddNewProfessorInput : IRequest<Response>
    {
        [Required]
        public int ProfessorId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Specialty { get; set; }
    }
}
