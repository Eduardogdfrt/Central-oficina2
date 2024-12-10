using MediatR;
using Microsoft.Extensions.Logging;
using Ellp.Api.Application.Interfaces;
using Ellp.Api.Domain.Entities;
using Ellp.Api.Application.Utilities;

namespace Ellp.Api.Application.UseCases.Users.AddParticipantUsecases.AddNewProfessorUseCases
{
    public class AddNewProfessorUseCase : IRequestHandler<AddNewProfessorInput, Response>
    {
        private readonly ILogger<AddNewProfessorUseCase> _logger;
        private readonly IProfessorRepository _professorRepository;

        public AddNewProfessorUseCase(ILogger<AddNewProfessorUseCase> logger, IProfessorRepository professorRepository)
        {
            _logger = logger;
            _professorRepository = professorRepository;
        }

        public async Task<Response> Handle(AddNewProfessorInput request, CancellationToken cancellationToken)
        {
            try
            {

                var existingProfessor = await _professorRepository.GetByEmailAsync(request.Email);
                if (existingProfessor != null)
                {
                    return new Response { Message = "Email já está em uso" };
                }

      

        
                var newProfessor = new Professor(
                    professorId: request.ProfessorId, 
                    name: request.Name,
                    specialty: request.Specialty,
                    password: request.Password,
                    email: request.Email
                );

                await _professorRepository.AddNewProfessorAsync(newProfessor);

                return new Response { Message = "Professor criado com  ID "+ newProfessor.ProfessorId+ " criado com sucesso" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao adicionar um novo professor.");
                return new Response { Message = "Ocorreu um erro durante o processamento" };
            }
        }
    }
}
