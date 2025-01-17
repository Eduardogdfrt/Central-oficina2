using MediatR;

namespace Ellp.Api.Application.UseCases.StudentWorkshop.GetAllStudentsForWorkshop
{
    public class GetAllStudentsForWorkshopInput : IRequest<GetAllStudentsForWorkshopOutput>
    {
        public int WorkshopId { get; set; }
    }
}
















