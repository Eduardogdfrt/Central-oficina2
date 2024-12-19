using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Ellp.Api.Application.UseCases.Workshops.AddWorkshops
{
    public class AddWorkshopInput : IRequest<AddWorkshopOutput>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int ProfessorId { get; set; }
        [Required]
        public DateTime Data { get; set; }

        public int HelperId { get; set; }
    }
}






