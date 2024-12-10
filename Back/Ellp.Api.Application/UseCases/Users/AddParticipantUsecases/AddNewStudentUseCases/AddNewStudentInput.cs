using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Ellp.Api.Application.Utilities;
namespace Ellp.Api.Application.UseCases.Users.AddParticipantUsecases.AddNewStudentUseCases
{
    public class AddNewStudentInput : IRequest<Response>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}
