using MediatR;
namespace Ellp.Api.Application.UseCases.StudentWorkshop.EmitirCertificadosEmLote
{
    public class EmitirCertificadosEmLoteInput : IRequest<EmitirCertificadosEmLoteOutput>
    {
        public List<CertificadoRequest> Certificados { get; set; }
    }

    public class CertificadoRequest
    {
        public int StudentId { get; set; }
        public int WorkshopId { get; set; }
    }
}


