using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ellp.Api.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Ellp.Api.Application.UseCases.StudentWorkshop.GetStudentWorkshop
{
    public class GetStudentWorkshopUseCase : IRequestHandler<GetStudentWorkshopInput, GetStudentWorkshopOutput>
    {
        private readonly IStudentWorkshopRepository _studentWorkshopRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IWorkshopRepository _workshopRepository;
        private readonly ILogger<GetStudentWorkshopUseCase> _logger;

        public GetStudentWorkshopUseCase(
            IStudentWorkshopRepository studentWorkshopRepository,
            IStudentRepository studentRepository,
            IWorkshopRepository workshopRepository,
            ILogger<GetStudentWorkshopUseCase> logger)
        {
            _studentWorkshopRepository = studentWorkshopRepository;
            _studentRepository = studentRepository;
            _workshopRepository = workshopRepository;
            _logger = logger;
        }

        public async Task<GetStudentWorkshopOutput> Handle(GetStudentWorkshopInput request, CancellationToken cancellationToken)
        {
            try
            {
                var studentWorkshop = await _studentWorkshopRepository.GetStudentWorkshopAsync(request.StudentId, request.WorkshopId);
                return studentWorkshop != null
                    ? GetStudentWorkshopOutput.CreateOutput(true, "Relação encontrada", studentWorkshop)
                    : GetStudentWorkshopOutput.CreateOutput(false, "Relação entre aluno e workshop não encontrada");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao buscar a relação entre o aluno e o workshop.");
                return GetStudentWorkshopOutput.CreateOutput(false, "Ocorreu um erro ao buscar a relação entre o aluno e o workshop: " + ex.Message);
            }
        }
    }
}






