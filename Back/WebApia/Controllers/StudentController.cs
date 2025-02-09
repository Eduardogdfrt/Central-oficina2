using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AppResponse = Ellp.Api.Application.Utilities.Response;
using Ellp.Api.Application.UseCases.Users.AddParticipantUsecases.AddNewStudentUseCases;
using Ellp.Api.Application.UseCases.Users.GetLoginUseCases.GetLoginStudent;
using Ellp.Api.Application.UseCases.Users.SearchAllStudents;

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

        [HttpGet("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLoginStudent(
            [Required][FromQuery] string email,
            [FromQuery] string? password = null,
            CancellationToken cancellationToken = default)
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
                return StatusCode(StatusCodes.Status500InternalServerError, new AppResponse { Message = "Ocorreu um erro durante o processamento" });
            }
        }

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNewStudent([FromBody] AddNewStudentInput input, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(input, cancellationToken);

                if (response.Message == "Estudante criado com sucesso")
                {
                    return CreatedAtAction(nameof(GetLoginStudent), new { email = input.Email }, response);
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
                _logger.LogError(ex, "Ocorreu um erro ao adicionar um novo estudante.");
                return StatusCode(StatusCodes.Status500InternalServerError, new AppResponse { Message = "Ocorreu um erro durante o processamento" });
            }
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SearchAllStudents(
            [FromQuery] int? studentId = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var input = new SearchAllStudentsInput(studentId);
                var result = await _mediator.Send(input, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao buscar os estudantes.");
                return StatusCode(StatusCodes.Status500InternalServerError, new AppResponse { Message = "Ocorreu um erro durante o processamento" });
            }
        }
    }
}

