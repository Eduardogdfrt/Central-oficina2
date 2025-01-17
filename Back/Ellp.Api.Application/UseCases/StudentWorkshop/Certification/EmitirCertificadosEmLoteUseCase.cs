using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ellp.Api.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Ellp.Api.Application.UseCases.StudentWorkshop.EmitirCertificadosEmLote
{
    public class EmitirCertificadosEmLoteUseCase : IRequestHandler<EmitirCertificadosEmLoteInput, EmitirCertificadosEmLoteOutput>
    {
        private readonly IStudentWorkshopRepository _studentWorkshopRepository;
        private readonly ILogger<EmitirCertificadosEmLoteUseCase> _logger;

        public EmitirCertificadosEmLoteUseCase(IStudentWorkshopRepository studentWorkshopRepository, ILogger<EmitirCertificadosEmLoteUseCase> logger)
        {
            _studentWorkshopRepository = studentWorkshopRepository;
            _logger = logger;
        }

        public async Task<EmitirCertificadosEmLoteOutput> Handle(EmitirCertificadosEmLoteInput request, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var certificado in request.Certificados)
                {
                    await _studentWorkshopRepository.EmitirCertificadoAsync(certificado.StudentId, certificado.WorkshopId, certificado.Certificado);
                }

                return new EmitirCertificadosEmLoteOutput { Success = true, Message = "Certificados emitidos com sucesso" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao emitir os certificados.");
                return new EmitirCertificadosEmLoteOutput { Success = false, Message = "Ocorreu um erro ao emitir os certificados" };
            }
        }
    }
}


