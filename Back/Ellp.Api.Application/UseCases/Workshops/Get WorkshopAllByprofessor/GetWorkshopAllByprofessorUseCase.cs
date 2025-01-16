using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ellp.Api.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Ellp.Api.Application.UseCases.Workshops.GetWorkshopAll
{
    public class GetWorkshopAllByprofessorUseCase : IRequestHandler<GetWorkshopAllByprofessorInput, GetWorkshopAllByprofessorOutput>
    {
        private readonly ILogger<GetWorkshopAllByprofessorUseCase> _logger;
        private readonly IWorkshopRepository _workshopRepository;

        public GetWorkshopAllByprofessorUseCase(ILogger<GetWorkshopAllByprofessorUseCase> logger, IWorkshopRepository workshopRepository)
        {
            _logger = logger;
            _workshopRepository = workshopRepository;
        }

        public async Task<GetWorkshopAllByprofessorOutput> Handle(GetWorkshopAllByprofessorInput request, CancellationToken cancellationToken)
        {
            try
            {
                var workshopList = await _workshopRepository.GetWorkshopAllforProfessorsAsync(request.professorId);


                return new GetWorkshopAllByprofessorOutput { Workshops = workshopList };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao buscar os workshops.");
                throw;
            }
        }
    }
}










