using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Ellp.Api.Application.UseCases.StudentWorkshop.AddStundentWorkshop;
using Ellp.Api.Application.UseCases.StudentWorkshop.DeleteUserForWorkShop;
using Ellp.Api.Application.UseCases.StudentWorkshop.GetStudentWorkshop;
using Ellp.Api.Application.UseCases.StudentWorkshop.GetAllStudentWorkshops;
using Ellp.Api.Application.UseCases.StudentWorkshop.GetAllStudentsForWorkshop;
using Ellp.Api.Application.UseCases.StudentWorkshop.EmitirCertificadosEmLote;

namespace Ellp.Api.Webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkshopStudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkshopStudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddStudentWorkshop([FromBody] AddStudentWorkshopInput input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _mediator.Send(input);

            if (response.Success)
            {
                return CreatedAtAction(nameof(AddStudentWorkshop), new { studentId = input.StudentId, workshopId = input.WorkshopId }, response);
            }

            return BadRequest(response);
        }

        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteStudentWorkshop([FromBody] DeleteStudentWorkshopInput input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _mediator.Send(input);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("{studentId}/{workshopId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStudentWorkshop(int studentId, int workshopId)
        {
            var input = new GetStudentWorkshopInput { StudentId = studentId, WorkshopId = workshopId };
            var response = await _mediator.Send(input);

            if (response.Success)
            {
                return Ok(response);
            }

            return NotFound(response);
        }

        [HttpGet("{studentId}/workshops")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllStudentWorkshops(int studentId)
        {
            var input = new GetAllStudentWorkshopsInput { StudentId = studentId };
            var response = await _mediator.Send(input);

            if (response.Success)
            {
                return Ok(response);
            }

            return NotFound(response);
        }

        [HttpGet("workshop/{workshopId}/students")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllStudentsForWorkshop(int workshopId)
        {
            var input = new GetAllStudentsForWorkshopInput { WorkshopId = workshopId };
            var response = await _mediator.Send(input);

            if (response.Success)
            {
                return Ok(response);
            }

            return NotFound(response);
        }

        [HttpPost("emitir-certificado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EmitirCertificado([FromBody] EmitirCertificadosEmLoteInput input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _mediator.Send(input);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}

