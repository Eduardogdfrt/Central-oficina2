using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ellp.Api.Application.Interfaces;
using Ellp.Api.Domain.Entities;

namespace Ellp.Api.Application.UseCases.Users.GetLoginUseCases.GetLoginStudent
{
    public class GetLoginStudentUseCase : IRequestHandler<GetLoginStudentInput, GetLoginStudentOutput>
    {
        private readonly ILogger<GetLoginStudentUseCase> _logger;
        private readonly IStudentRepository _studentRepository;

        public GetLoginStudentUseCase(ILogger<GetLoginStudentUseCase> logger, IStudentRepository studentRepository)
        {
            _logger = logger;
            _studentRepository = studentRepository;
        }

        public async Task<GetLoginStudentOutput> Handle(GetLoginStudentInput request, CancellationToken cancellationToken)
        {
            try
            {
                var student = await _studentRepository.GetStudentByEmailAsync(request.Email);

                if (student == null)
                {
                    return new GetLoginStudentOutput
                    {
                        Success = false,
                        Message = "Email inválido ou não autenticado"
                    };
                }

                if (student.IsAuthenticated)
                {
                    if (string.IsNullOrEmpty(request.Password))
                    {
                        return new GetLoginStudentOutput
                        {
                            Success = false,
                            Message = "Senha é obrigatória para login"
                        };
                    }
                    student = await _studentRepository.GetStudentByEmailAndPasswordAsync(request.Email, request.Password);
                    if (student == null)
                    {
                        return new GetLoginStudentOutput
                        {
                            Success = false,
                            Message = "Email ou senha inválidos"
                        };
                    }
                    return GetLoginStudentOutput.ToLoginOutput(student);
                }
                else
                {
                    return GetLoginStudentOutput.ToLoginOutput(student);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao processar a solicitação de login.");
                return new GetLoginStudentOutput
                {
                    Success = false,
                    Message = "Ocorreu um erro durante o processamento"
                };
            }
        }
    }
}
