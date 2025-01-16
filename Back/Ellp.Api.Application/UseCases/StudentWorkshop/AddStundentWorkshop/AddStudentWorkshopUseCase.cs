using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ellp.Api.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Ellp.Api.Application.UseCases.StudentWorkshop.AddStundentWorkshop
{
    public class AddStudentWorkshopUseCase : IRequestHandler<AddStudentWorkshopInput, AddStudentWorkshopOutput>
    {
        private readonly ILogger<AddStudentWorkshopUseCase> _logger;
        private readonly IStudentWorkshopRepository _studentWorkshopRepository;

        public AddStudentWorkshopUseCase(ILogger<AddStudentWorkshopUseCase> logger, IStudentWorkshopRepository studentWorkshopRepository)
        {
            _logger = logger;
            _studentWorkshopRepository = studentWorkshopRepository;
        }

        public async Task<AddStudentWorkshopOutput> Handle(AddStudentWorkshopInput request, CancellationToken cancellationToken)
        {
            try
            {
                // Adiciona o workshop ao estudante
                await _studentWorkshopRepository.AddStudentWorkshopAsync(request.StudentId, request.WorkshopId);

                return new AddStudentWorkshopOutput
                {
                    Success = true,
                    Message = "Workshop adicionado ao estudante com sucesso"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao adicionar o workshop ao estudante.");
                return new AddStudentWorkshopOutput
                {
                    Success = false,
                    Message = "Ocorreu um erro ao adicionar o workshop ao estudante"
                };
            }
        }
    }
}
