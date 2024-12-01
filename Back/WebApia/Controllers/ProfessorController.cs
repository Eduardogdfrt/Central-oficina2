using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Ellp.Api.Application.UseCases.AddParticipantUsecases.AddNewProfessorUseCases;
using Ellp.Api.Application.Utilities;
using Ellp.Api.Application.UseCases.GetLoginUseCases.GetLoginProfessor;

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
            var response = await _mediator.Send(input, cancellationToken);

            if (response.Message == "Professor "+ input.Name + "Foi criado com sucesso seu ID é" + input.ProfessorId)
            {
                return StatusCode(StatusCodes.Status201Created, response);
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
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new Response { Message = result.Message });
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
