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
                if (studentWorkshop == null)
                {
                    return new GetStudentWorkshopOutput
                    {
                        Success = false,
                        Message = "Relação entre aluno e workshop não encontrada"
                    };
                }

                var workshop = await _workshopRepository.GetWorkshopByIdAsync(request.WorkshopId);
                var student = await _studentRepository.GetStudentByIdAsync(request.StudentId);


                return new GetStudentWorkshopOutput
                {
                    Success = true,
                    Message = "Relação encontrada",
                    Data = studentWorkshop,
       
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao buscar a relação entre o aluno e o workshop.");
                return new GetStudentWorkshopOutput
                {
                    Success = false,
                    Message = "Ocorreu um erro ao buscar a relação entre o aluno e o workshop: " + ex.Message
                };
            }
        }
    }
}
