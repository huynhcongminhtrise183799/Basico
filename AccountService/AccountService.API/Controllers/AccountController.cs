using AccountService.Application.Commands;
using AccountService.Application.DTOs.Request;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AccountService.API.Controllers{

	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IMediator _mediator;

		public AccountController(IMediator mediator)
		{
			_mediator = mediator;
		}


		[HttpPost("signin-google")]
		public async Task<IActionResult> LoginWithGoogle([FromBody] GoogleLoginRequest request)
		{
			try
			{
				var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken);

				var email = payload.Email;
				var name = payload.Name;

				var command = new GoogleLoginCommand(email, name);
				var response = await _mediator.Send(command);

				return Ok(response);
			}
			catch (InvalidJwtException)
			{
				return BadRequest(new { message = "Invalid Google token" });
			}
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterAccountCommand command)
		{
			if (!ModelState.IsValid)
			{
				var firstError = ModelState.Values
				.SelectMany(v => v.Errors)
				.Select(e => e.ErrorMessage)
				.FirstOrDefault();

				return BadRequest(new { message = firstError });
			}

			try
			{
				var accountId = await _mediator.Send(command);
				return Ok(new { AccountId = accountId });
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}

		}
	}
}
