using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ellp.Api.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Ellp.Api.Application.UseCases.StudentWorkshop.GetAllStudentWorkshops
{
    public class GetAllStudentWorkshopsUseCase : IRequestHandler<GetAllStudentWorkshopsInput, GetAllStudentWorkshopsOutput>
    {
        private readonly IStudentWorkshopRepository _studentWorkshopRepository;
        private readonly ILogger<GetAllStudentWorkshopsUseCase> _logger;

        public GetAllStudentWorkshopsUseCase(IStudentWorkshopRepository studentWorkshopRepository, ILogger<GetAllStudentWorkshopsUseCase> logger)
        {
            _studentWorkshopRepository = studentWorkshopRepository;
            _logger = logger;
        }

        public async Task<GetAllStudentWorkshopsOutput> Handle(GetAllStudentWorkshopsInput request, CancellationToken cancellationToken)
        {
            try
            {
                var workshops = await _studentWorkshopRepository.GetAllWorkshopsByStudentIdAsync(request.StudentId);
                return workshops != null && workshops.Count > 0
                    ? new GetAllStudentWorkshopsOutput { Success = true, Message = "Workshops encontrados", Workshops = workshops }
                    : new GetAllStudentWorkshopsOutput { Success = false, Message = "Nenhum workshop encontrado para o aluno" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao buscar os workshops do aluno.");
                return new GetAllStudentWorkshopsOutput { Success = false, Message = "Ocorreu um erro ao buscar os workshops do aluno" + ex };
            }
        }
    }
}














