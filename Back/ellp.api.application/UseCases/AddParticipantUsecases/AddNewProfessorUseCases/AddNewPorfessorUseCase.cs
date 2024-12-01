using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ellp.Api.Application.Interfaces;
using Ellp.Api.Domain.Entities;
using Ellp.Api.Application.Utilities;

namespace Ellp.Api.Application.UseCases.AddParticipantUsecases.AddNewProfessorUseCases
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
                // Verificar se o email já está cadastrado
                var existingProfessor = await _professorRepository.GetByEmailAsync(request.Email);
                if (existingProfessor != null)
                {
                    return new Response { Message = "Email já está em uso" };
                }

                // Hashear a senha
                string hashedPassword = PasswordHasher.HashPassword(request.Password);

                // Criar uma nova instância de Professor usando o construtor
                var newProfessor = new Professor(
                    professorId: request.ProfessorId, // Usar o ID fornecido
                    name: request.Name,
                    specialty: request.Specialty,
                    password: hashedPassword,
                    email: request.Email
                );

                // Adicionar ao repositório
                await _professorRepository.AddNewProfessorAsync(newProfessor);

                return new Response { Message = "Professor criado com sucesso" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao adicionar um novo professor.");
                return new Response { Message = "Ocorreu um erro durante o processamento" };
            }
        }
    }
}
