using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ellp.Api.Domain.Entities;
using Ellp.Api.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Ellp.Api.Application.UseCases.Workshops.AddWorkshops
{
    public class AddWorkshopUseCase : IRequestHandler<AddWorkshopInput, AddWorkshopOutput>
    {
        private readonly ILogger<AddWorkshopUseCase> _logger;
        private readonly IWorkshopRepository _workshopRepository;

        public AddWorkshopUseCase(ILogger<AddWorkshopUseCase> logger, IWorkshopRepository workshopRepository)
        {
            _logger = logger;
            _workshopRepository = workshopRepository;
        }

        public async Task<AddWorkshopOutput> Handle(AddWorkshopInput request, CancellationToken cancellationToken)
        {
            try
            {
                var newWorkshop = new Workshop
                {
                    Name = request.Name,
                    Data = request.Data,
                    ProfessorIdW = request.ProfessorId,
                    HelperIDW = request.HelperId 
                };

                await _workshopRepository.AddAsync(newWorkshop);

                return new AddWorkshopOutput
                {
                    Id = newWorkshop.Id,
                    Name = newWorkshop.Name,
                    Message = "Workshop criado com sucesso"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao adicionar um novo workshop.");
                return new AddWorkshopOutput { Message = "Ocorreu um erro durante o processamento "  + ex};
            }
        }
    }
}
