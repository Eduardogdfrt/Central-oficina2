using MediatR;

namespace Ellp.Api.Application.UseCases.StudentWorkshop.GetAllStudentWorkshops
{
    public class GetAllStudentWorkshopsInput : IRequest<GetAllStudentWorkshopsOutput>
    {
        public int StudentId { get; set; }
    }
}














