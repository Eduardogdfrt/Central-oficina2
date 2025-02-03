using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ellp.Api.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Ellp.Api.Application.UseCases.StudentWorkshop.GetAllStudentsForWorkshop
{
    public class GetAllStudentsForWorkshopUseCase : IRequestHandler<GetAllStudentsForWorkshopInput, GetAllStudentsForWorkshopOutput>
    {
        private readonly IStudentWorkshopRepository _studentWorkshopRepository;
        private readonly ILogger<GetAllStudentsForWorkshopUseCase> _logger;

        public GetAllStudentsForWorkshopUseCase(IStudentWorkshopRepository studentWorkshopRepository, ILogger<GetAllStudentsForWorkshopUseCase> logger)
        {
            _studentWorkshopRepository = studentWorkshopRepository;
            _logger = logger;
        }

        public async Task<GetAllStudentsForWorkshopOutput> Handle(GetAllStudentsForWorkshopInput request, CancellationToken cancellationToken)
        {
            try
            {
                var students = await _studentWorkshopRepository.GetAllStudentsByWorkshopIdAsync(request.WorkshopId);
                return students != null && students.Count > 0
                    ? GetAllStudentsForWorkshopOutput.CreateOutput(true, "Alunos encontrados", students)
                    : GetAllStudentsForWorkshopOutput.CreateOutput(false, "Nenhum aluno encontrado para o workshop");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao buscar os alunos do workshop.");
                return GetAllStudentsForWorkshopOutput.CreateOutput(false, "Ocorreu um erro ao buscar os alunos do workshop: " + ex.Message);
            }
        }
    }
}








