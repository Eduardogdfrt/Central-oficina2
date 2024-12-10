using MediatR;
using Microsoft.Extensions.Logging;
using Ellp.Api.Application.Interfaces;
using Ellp.Api.Application.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace Ellp.Api.Application.UseCases.Users.GetLoginUseCases.GetLoginProfessor
{
    public class GetLoginProfessorUseCase : IRequestHandler<GetLoginProfessorInput, GetLoginProfessorOutput>
    {
        private readonly ILogger<GetLoginProfessorUseCase> _logger;
        private readonly IProfessorRepository _professorRepository;

        public GetLoginProfessorUseCase(ILogger<GetLoginProfessorUseCase> logger, IProfessorRepository professorRepository)
        {
            _logger = logger;
            _professorRepository = professorRepository;
        }

        public async Task<GetLoginProfessorOutput> Handle(GetLoginProfessorInput request, CancellationToken cancellationToken)
        {
            try
            {
                var professor = await _professorRepository.GetAllProfessorInfosAsync(request.ProfessorId, request.Password);
                if (professor == null || !PasswordHasher.VerifyPassword(request.Password, professor.Password))
                {
                    return new GetLoginProfessorOutput { Success = false, Message = "Invalid professor ID or password" };
                }

                return GetLoginProfessorOutput.ToLoginOutput(professor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the login request.");
                return new GetLoginProfessorOutput { Success = false, Message = "An error occurred" };
            }
        }
    }
}
