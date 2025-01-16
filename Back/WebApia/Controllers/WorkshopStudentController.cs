using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Ellp.Api.Application.UseCases.StudentWorkshop.AddStundentWorkshop;

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
    }
}

