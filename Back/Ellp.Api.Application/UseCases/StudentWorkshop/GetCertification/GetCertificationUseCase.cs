using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ellp.Api.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Ellp.Api.Application.UseCases.StudentWorkshop.GetCertification
{
    public class GetCertificationUseCase : IRequestHandler<GetCertificationInput, GetCertificationOutput>
    {
        private readonly IStudentWorkshopRepository _studentWorkshopRepository;
        private readonly ILogger<GetCertificationUseCase> _logger;

        public GetCertificationUseCase(IStudentWorkshopRepository studentWorkshopRepository, ILogger<GetCertificationUseCase> logger)
        {
            _studentWorkshopRepository = studentWorkshopRepository;
            _logger = logger;
        }

        public async Task<GetCertificationOutput> Handle(GetCertificationInput request, CancellationToken cancellationToken)
        {
            try
            {
                var studentWorkshop = await _studentWorkshopRepository.GetStudentWorkshopAsync(request.StudentId, request.WorkshopId);
                return studentWorkshop != null && !string.IsNullOrEmpty(studentWorkshop.Certificate)
                    ? GetCertificationOutput.CreateOutput(true, "Certificação encontrada", studentWorkshop.Certificate)
                    : GetCertificationOutput.CreateOutput(false, "Certificação não encontrada");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao buscar a certificação.");
                return GetCertificationOutput.CreateOutput(false, "Ocorreu um erro ao buscar a certificação: " + ex.Message);
            }
        }
    }
}





