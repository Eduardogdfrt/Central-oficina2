using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Ellp.Api.Application.UseCases.AddWorkshops;
using Ellp.Api.Application.Utilities;

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

        [HttpPost]
        public async Task<IActionResult> AddWorkshop([FromBody] AddWorkshopInput input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _mediator.Send(input);

            if (response.Message == "Workshop criado com sucesso")
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}








































































