using AccountService.Application.Commands;
using AccountService.Application.Commands.StaffCommands;
using AccountService.Application.DTOs;
using AccountService.Application.DTOs.Request;
using AccountService.Application.Queries;
using AccountService.Application.Queries.StaffQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AccountService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StaffController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StaffCreateRequest request)
        {
            var command = new CreateStaffCommand(request.FullName, request.Email, request.Gender, request.Password, request.Username, request.ImageUrl);
            var staffId = await _mediator.Send(command);
            return Ok(new { StaffId = staffId });
        }
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] StaffUpdateRequest request)
		{
			var command = new UpdateStaffCommand(request.StaffId, request.FullName, request.Gender, request.ImageUrl);
			var result = await _mediator.Send(command);
			if (!result) return BadRequest(new { message = "Update failed" });
			return Ok(new { message = "Update successful" });
		}


		[HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteStaffCommand(id);
            var result = await _mediator.Send(command);
            if (!result) return BadRequest(new { message = "Delete failed" });
            return Ok(new { message = "Delete successful" });
        }

		[HttpGet]
		public async Task<IActionResult> GetAllStaff([FromQuery] PaginationQuery pagination)
		{
			if (pagination.Page < 1 || pagination.PageSize < 1)
			{
				return BadRequest(new { message = "Page and PageSize must be greater than 0." });
			}

			int defaultPageSize = 2;

			var query = new GetAllStaffQuery
			{
				Page = pagination.Page,
				PageSize = pagination.PageSize ?? defaultPageSize
			};

			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpGet("Active")]
		public async Task<IActionResult> GetActiveStaff([FromQuery] PaginationQuery pagination)
		{
			if (pagination.Page < 1 || pagination.PageSize < 1)
			{
				return BadRequest(new { message = "Page and PageSize must be greater than 0." });
			}

			int defaultPageSize = 2;

			var query = new GetAllActiveStaffQuery
			{
				Page = pagination.Page,
				PageSize = pagination.PageSize ?? defaultPageSize
			};

			var result = await _mediator.Send(query);
			return Ok(result);
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetStaffById(Guid id)
		{
			var accountId = User.FindFirstValue(ClaimTypes.Sid);
			if (string.IsNullOrEmpty(accountId))
			{
				return Unauthorized(new
				{
					message = "Unauthorized access. Please log in to update your profile."
				});
			}
			var query = new GetStaffByIdQuery(id);
			var staff = await _mediator.Send(query); // Bạn sẽ cần tạo GetStaffByIdQuery
			if (staff == null) return NotFound();
			return Ok(staff);
		}
	}
}