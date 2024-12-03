using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ellp.Api.Domain.Entities;
using Ellp.Api.Application.Interfaces;
using Ellp.Api.Application.Utilities;
using Microsoft.Extensions.Logging;

namespace Ellp.Api.Application.UseCases.AddWorkshops
{
    public class AddWorkshopUseCase : IRequestHandler<AddWorkshopInput, Response>
    {
        private readonly ILogger<AddWorkshopUseCase> _logger;
        private readonly IWorkshopRepository _workshopRepository;

        public AddWorkshopUseCase(ILogger<AddWorkshopUseCase> logger, IWorkshopRepository workshopRepository)
        {
            _logger = logger;
            _workshopRepository = workshopRepository;
        }

        public async Task<Response> Handle(AddWorkshopInput request, CancellationToken cancellationToken)
        {
            try
            {
                var newWorkshop = AddWorkshopMapper.ToEntity(request);

                await _workshopRepository.AddAsync(newWorkshop);

                return new Response { Message = "Workshop criado com sucesso" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao adicionar um novo workshop.");
                return new Response { Message = "Ocorreu um erro durante o processamento" };
            }
        }
    }
}
