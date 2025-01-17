using MediatR;

namespace Ellp.Api.Application.UseCases.StudentWorkshop.GetStudentWorkshop
{
    public class GetStudentWorkshopInput : IRequest<GetStudentWorkshopOutput>
    {
        public int StudentId { get; set; }
        public int WorkshopId { get; set; }
    }
}













