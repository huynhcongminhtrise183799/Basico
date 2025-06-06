using AccountService.Application.Commands;
using AccountService.Application.DTOs.Request;
using AccountService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AccountService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
           var command = new LoginUserCommand(request.UserName, request.Password);
           var result = await _mediator.Send(command);
            if (result == null)
            {
                return BadRequest(new
                {
                    message = "Invalid username or password."
                });
            }
            return Ok(new { Token = result });
        }

        [HttpGet]
        [Route("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var accountId = User.FindFirstValue(ClaimTypes.Sid);
            if (string.IsNullOrEmpty(accountId))
            {
                return Unauthorized(new
                {
                    message = "Unauthorized access. Please log in to view your profile."
                });
            }
            var query = new ProfileQuery(Guid.Parse(accountId));
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return BadRequest("Profile not found.");
            }
            return Ok(result);
        }
    }
}
