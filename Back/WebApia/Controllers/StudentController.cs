using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Ellp.Api.Application.UseCases.GetLoginUseCases.GetLoginStudent;

namespace Ellp.Api.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IMediator _mediator;

        public StudentController(ILogger<StudentController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // Rota para login de estudante
        [HttpGet("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLoginStudent(
            [Required][FromQuery] string email,
            [FromQuery] string? password = null, // Parâmetro opcional com valor padrão
            CancellationToken cancellationToken = default) // Adicionado valor padrão
        {
            try
            {
                GetLoginStudentInput input = string.IsNullOrEmpty(password)
                    ? new GetLoginStudentInput { Email = email }
                    : new GetLoginStudentInput { Email = email, Password = password };

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






