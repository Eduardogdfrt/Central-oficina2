using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Ellp.Api.Application.UseCases.Users.AddParticipantUsecases.AddNewProfessorUseCases
{
    public class AddNewProfessorInput : IRequest<AddNewProfessorOutput>
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


