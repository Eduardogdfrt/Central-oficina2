using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ellp.Api.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Ellp.Api.Application.UseCases.Workshops.GetWorkshopAll
{
    public class GetWorkshopAllUseCase : IRequestHandler<GetWorkshopAllInput, GetWorkshopAllOutput>
    {
        private readonly ILogger<GetWorkshopAllUseCase> _logger;
        private readonly IWorkshopRepository _workshopRepository;

        public GetWorkshopAllUseCase(ILogger<GetWorkshopAllUseCase> logger, IWorkshopRepository workshopRepository)
        {
            _logger = logger;
            _workshopRepository = workshopRepository;
        }

        public async Task<GetWorkshopAllOutput> Handle(GetWorkshopAllInput request, CancellationToken cancellationToken)
        {
            try
            {
                var workshopList = await _workshopRepository.GetWorkshopAllAsync();
                return new GetWorkshopAllOutput { Workshops = workshopList };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao buscar os workshops.");
                throw;
            }
        }
    }
}
