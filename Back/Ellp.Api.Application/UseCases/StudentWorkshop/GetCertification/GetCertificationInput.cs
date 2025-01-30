using MediatR;

namespace Ellp.Api.Application.UseCases.StudentWorkshop.GetCertification
{
    public class GetCertificationInput : IRequest<GetCertificationOutput>
    {
        public int StudentId { get; set; }
        public int WorkshopId { get; set; }
    }
}



