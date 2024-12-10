using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ellp.Api.Application.Interfaces;
using Ellp.Api.Domain.Entities;

namespace Ellp.Api.Application.UseCases.Workshops.GetWorkshopById
{
    public class GetWorkshopByIdUseCase : IRequestHandler<GetWorkshopByIdInput, GetWorkshopByIdOutput>
    {
        private readonly ILogger<GetWorkshopByIdUseCase> _logger;
        private readonly IWorkshopRepository _workshopRepository;

        public GetWorkshopByIdUseCase(ILogger<GetWorkshopByIdUseCase> logger, IWorkshopRepository workshopRepository)
        {
            _logger = logger;
            _workshopRepository = workshopRepository;
        }

        public async Task<GetWorkshopByIdOutput> Handle(GetWorkshopByIdInput request, CancellationToken cancellationToken)
        {
            try
            {
                var workshop = await _workshopRepository.GetWorkshopByIdAsync(request.Id);

                if (workshop == null)
                {
                    return GetWorkshopByIdOutput.ToOutputIfNotFound();
                }

                return GetWorkshopByIdOutput.ToOutput(workshop);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao buscar o workshop.");
                return new GetWorkshopByIdOutput
                {
                    Success = false,
                    Message = "Ocorreu um erro durante o processamento"
                };
            }
        }
    }
}
