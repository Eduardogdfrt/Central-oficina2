using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Ellp.Api.Application.UseCases.Users.GetLoginUseCases.GetLoginProfessor;
using Ellp.Api.Application.UseCases.Users.AddParticipantUsecases.AddNewProfessorUseCases;

namespace Ellp.Api.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly ILogger<ProfessorController> _logger;
        private readonly IMediator _mediator;

        public ProfessorController(ILogger<ProfessorController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNewProfessor([FromBody] AddNewProfessorInput input, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(input, cancellationToken);

                if (response.Message == "Professor criado com sucesso")
                {
                    // Retorna CreatedAtAction para apontar para o método GetLoginProfessor
                    return CreatedAtAction(
                        nameof(GetLoginProfessor),
                        new { professorId = input.ProfessorId },
                        response);
                }
                else if (response.Message == "Email já está em uso")
                {
                    return BadRequest(response);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao adicionar um novo professor.");
               return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLoginProfessor(
           [Required][FromQuery] int professorId,
           [Required][FromQuery] string password,
           CancellationToken cancellationToken)
        {
            try
            {
                var input = new GetLoginProfessorInput { ProfessorId = professorId, Password = password };
                var result = await _mediator.Send(input, cancellationToken);

                if (result.Success)
                {
                    return Ok(result); // Explicitamente retorna OkObjectResult
                }
                else
                {
                    return BadRequest(new Response { Message = result.Message }); // Explicitamente retorna BadRequestObjectResult
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao processar a solicitação de login.");
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Message = "Ocorreu um erro durante o processamento" });
            }
        }
    }
}
