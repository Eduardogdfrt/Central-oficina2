using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ellp.Api.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Ellp.Api.Application.UseCases.StudentWorkshop.DeleteUserForWorkShop
{
    public class DeleteStudentWorkshopUseCase : IRequestHandler<DeleteStudentWorkshopInput, DeleteStudentWorkshopOutput>
    {
        private readonly IStudentWorkshopRepository _studentWorkshopRepository;
        private readonly ILogger<DeleteStudentWorkshopUseCase> _logger;

        public DeleteStudentWorkshopUseCase(IStudentWorkshopRepository studentWorkshopRepository, ILogger<DeleteStudentWorkshopUseCase> logger)
        {
            _studentWorkshopRepository = studentWorkshopRepository;
            _logger = logger;
        }

        public async Task<DeleteStudentWorkshopOutput> Handle(DeleteStudentWorkshopInput request, CancellationToken cancellationToken)
        {
            try
            {
                var studentWorkshop = await _studentWorkshopRepository.GetStudentWorkshopAsync(request.StudentId, request.WorkshopId);
                if (studentWorkshop != null)
                {
                    await _studentWorkshopRepository.DeleteStudentWorkshopAsync(studentWorkshop);
                    return new DeleteStudentWorkshopOutput { Success = true, Message = "Aluno removido do workshop com sucesso" };
                }
                else
                {
                    return new DeleteStudentWorkshopOutput { Success = false, Message = "Relação entre aluno e workshop não encontrada" };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao remover o aluno do workshop.");
                return new DeleteStudentWorkshopOutput { Success = false, Message = "Ocorreu um erro ao remover o aluno do workshop" + ex };
            }
        }
    }
}












