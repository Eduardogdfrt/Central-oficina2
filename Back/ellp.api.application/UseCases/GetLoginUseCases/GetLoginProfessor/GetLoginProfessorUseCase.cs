using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ellp.Api.Application.Interfaces;

namespace Ellp.Api.Application.UseCases.GetLoginUseCases.GetLoginProfessor
{
    public class GetLoginProfessorUseCase : IRequestHandler<GetLoginProfessorInput, GetLoginProfessorMapper>
    {
        private readonly ILogger<GetLoginProfessorUseCase> _logger;
        private readonly IProfessorRepository _professorRepository;

        public GetLoginProfessorUseCase(ILogger<GetLoginProfessorUseCase> logger, IProfessorRepository professorRepository)
        {
            _logger = logger;
            _professorRepository = professorRepository;
        }

        public async Task<GetLoginProfessorMapper> Handle(GetLoginProfessorInput request, CancellationToken cancellationToken)
        {
            try
            {
                var professor = await _professorRepository.GetAllProfessorInfosAsync(request.ProfessorId, request.Password);
                if (professor == null)
                {
                    return new GetLoginProfessorMapper { Success = false, Message = "Invalid professor ID or password" };
                }

                return GetLoginProfessorMapper.ToLoginOutput(professor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the login request.");
                return new GetLoginProfessorMapper { Success = false, Message = "An error occurred" };
            }
        }
    }
}


