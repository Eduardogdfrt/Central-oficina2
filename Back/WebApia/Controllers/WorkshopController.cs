using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Ellp.Api.Application.UseCases.Workshops.AddWorkshops;
using Ellp.Api.Application.UseCases.Workshops.GetWorkshopById;
using Ellp.Api.Application.UseCases.Workshops.GetWorkshopAll;

namespace Ellp.Api.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkshopController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkshopController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddWorkshop([FromBody] AddWorkshopInput input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _mediator.Send(input);

            if (response.Message == "Workshop criado com sucesso")
            {
                return CreatedAtAction(nameof(GetWorkshopById), new { id = response.Id }, response);
            }

            return BadRequest(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWorkshopById(int id)
        {
            var input = new GetWorkshopByIdInput { Id = id };
            var result = await _mediator.Send(input);

            if (result.Success)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        [HttpGet("getAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWorkshopsByProfessorId()
        {
            var input = new GetWorkshopAllInput();
            var result = await _mediator.Send(input);

            if (result.Workshops != null && result.Workshops.Count > 0)
            {
                return Ok(result);
            }

            return NotFound(result);
        }
    }
}















