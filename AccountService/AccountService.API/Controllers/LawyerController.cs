using AccountService.Application.Commands.Lawyer;
using AccountService.Application.DTOs.LawyerDTO;
using AccountService.Application.Queries.Lawyer;
using AccountService.Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LawyerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LawyerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLawyer([FromBody] CreateLawyerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateLawyerCommand
            {
                Lawyer = dto
            };

            var id = await _mediator.Send(command);

            return Ok(new { Message = "Create Successfully !", Id = id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLawyer(Guid id, [FromBody] UpdateLawyerDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != dto.AccountId) return BadRequest();

            var command = new UpdateLawyerCommand(dto);
            await _mediator.Send(command);
            return Ok(new { Message = "Update Successfully !!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLawyer(Guid id)
        {
            var command = new DeleteLawyerCommand(id);
            await _mediator.Send(command);

            return Ok(new { Message = "Delete Successfully !!" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLawyerById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetLawyerByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLawyers()
        {
            var result = await _mediator.Send(new GetAllLawyersQuery());
            return Ok(result);
        }

        [HttpGet("active")]
        public async Task<ActionResult<List<Account>>> GetAllActive(CancellationToken cancellationToken)
        {
            var lawyers = await _mediator.Send(new GetAllActiveLawyerAccountsQuery(), cancellationToken);
            return Ok(lawyers);
        }
    }
}
