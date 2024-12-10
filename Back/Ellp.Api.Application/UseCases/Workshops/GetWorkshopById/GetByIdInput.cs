using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Ellp.Api.Application.UseCases.Workshops.GetWorkshopById
{
    public class GetWorkshopByIdInput : IRequest<GetWorkshopByIdOutput>
    {
        [Required]
        public int Id { get; set; }
    }
}
