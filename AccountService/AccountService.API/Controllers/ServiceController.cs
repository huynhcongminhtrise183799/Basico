using AccountService.Application.Commands.Service;
using Application.Services.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ServiceController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServiceCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { message = "Create Service successfully", id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateServiceCommand command)
        {
            command.ServiceId = id;
            await _mediator.Send(command);
            return Ok(new { message = "Update Service successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteServiceCommand { ServiceId = id });
            return Ok(new { message = "Delete Service successfully" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var services = await _mediator.Send(new GetAllServicesQuery());
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var service = await _mediator.Send(new GetServiceByIdQuery { ServiceId = id });
            if (service == null) return NotFound();
            return Ok(service);
        }
    }
}
