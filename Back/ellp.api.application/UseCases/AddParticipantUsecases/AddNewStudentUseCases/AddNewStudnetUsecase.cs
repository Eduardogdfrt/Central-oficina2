using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ellp.Api.Domain.Entities;
using Ellp.Api.Application.Interfaces;
using Ellp.Api.Application.Utilities;
using Microsoft.Extensions.Logging;

namespace Ellp.Api.Application.UseCases.AddParticipantUsecases.AddNewStudentUseCases
{
    public class AddNewStudentUseCase : IRequestHandler<AddNewStudentInput, Response>
    {
        private readonly ILogger<AddNewStudentUseCase> _logger;
        private readonly IStudentRepository _studentRepository;

        public AddNewStudentUseCase(ILogger<AddNewStudentUseCase> logger, IStudentRepository studentRepository)
        {
            _logger = logger;
            _studentRepository = studentRepository;
        }

        public async Task<Response> Handle(AddNewStudentInput request, CancellationToken cancellationToken)
        {
            try
            {
    
                var existingStudent = await _studentRepository.GetStudentByEmailAsync(request.Email);
                if (existingStudent != null)
                {
                    return new Response { Message = "Email já está em uso" };
                }


                var newStudent = AddNewStudentMapper.ToEntity(request);

  
                await _studentRepository.AddAsync(newStudent);

                return new Response { Message = "Estudante criado com sucesso" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao adicionar um novo estudante.");
                return new Response { Message = "Ocorreu um erro durante o processamento" };
            }
        }
    }
}
