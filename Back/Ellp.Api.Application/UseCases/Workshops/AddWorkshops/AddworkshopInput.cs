using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Ellp.Api.Application.Utilities;

namespace Ellp.Api.Application.UseCases.Workshops.AddWorkshops
{
    public class AddWorkshopInput : IRequest<Response>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Data { get; set; }
    }
}
