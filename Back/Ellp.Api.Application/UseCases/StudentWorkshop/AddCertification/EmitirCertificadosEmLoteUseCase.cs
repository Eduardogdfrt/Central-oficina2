using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ellp.Api.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Ellp.Api.Application.Utilities;

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
            var output = new EmitirCertificadosEmLoteOutput();

            try
            {
                foreach (var certificado in request.Certificados)
                {
                    var certificadoHash = GenerateCertificateHash.Generate(certificado.StudentId, certificado.WorkshopId);
                    await _studentWorkshopRepository.EmitirCertificadoAsync(certificado.StudentId, certificado.WorkshopId, certificadoHash);
                    output.HashCodes.Add(certificadoHash);
                }

                output.Success = true;
                output.Message = "Certificados emitidos com sucesso";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao emitir os certificados.");
                output.Success = false;
                output.Message = "Ocorreu um erro ao emitir os certificados";
            }

            return output;
        }
    }
}



