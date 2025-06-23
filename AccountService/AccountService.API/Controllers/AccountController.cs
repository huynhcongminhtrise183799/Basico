using AccountService.Application.Commands;
using AccountService.Application.Commands.AccountCommands;
using AccountService.Application.DTOs.Request;
using AccountService.Application.Queries;
using AccountService.Application.Queries.AccountQuery;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ForgotPasswordRequest = AccountService.Application.DTOs.Request.ForgotPasswordRequest;
using LoginRequest = AccountService.Application.DTOs.Request.LoginRequest;
using ResetPasswordRequest = AccountService.Application.DTOs.Request.ResetPasswordRequest;

namespace AccountService.API.Controllers{

	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IMediator _mediator;
		private const string MESSAGE_ACCOUNT_BANNED = "Account are banned";

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
			if (result == MESSAGE_ACCOUNT_BANNED)
			{
				return BadRequest(new
				{
					message = "Account is banned."
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
				return BadRequest(new
				{
					message = "Profile not found."
				});
			}
			return Ok(result);
		}


		[HttpPut]
		[Route("profile/update")]
		public async Task<IActionResult> UpdateProfile([FromBody] ProfileUpdateRequest request)
		{
			var accountId = User.FindFirstValue(ClaimTypes.Sid);
			if (string.IsNullOrEmpty(accountId))
			{
				return Unauthorized(new
				{
					message = "Unauthorized access. Please log in to update your profile."
				});
			}

			var command = new UpdateProfileCommand(Guid.Parse(accountId), request.FullName, request.Gender, request.Image);
			var result = await _mediator.Send(command);

			if (!result)
			{
				return BadRequest(new { message = "Profile update failed." });
			}

			return Ok(new { message = "Profile updated successfully." });
		}

		[HttpGet]
		[Route("all-user")]
		public async Task<IActionResult> GetAllUserAccounts()
		{
			var query = new GetAllAccountQuery();
			var accounts = await _mediator.Send(query);
			if (accounts == null || accounts.Count == 0)
			{
				return NotFound(new { message = "No accounts found." });
			}
			return Ok(accounts);
		}

		[HttpDelete("user/{id}")]
		public async Task<IActionResult> DeleteUserAccount(Guid id)
		{
			var command = new BanUserCommnad(id);
			var result = await _mediator.Send(command);
			if (!result)
			{
				return NotFound(new { message = "User account not found." });
			}
			return Ok(new { message = "User account deleted successfully." });
		}

		[HttpPut("active-user/{id}")]
		public async Task<IActionResult> ActiveUserAccount(Guid id)
		{
			var command = new ActiveUserAccountCommand(id);
			var result = await _mediator.Send(command);
			if (!result)
			{
				return NotFound(new { message = "User account not found." });
			}
			return Ok(new { message = "User account activated successfully." });
		}

		[HttpGet]
		public async Task<IActionResult> GetAccountByPhone([FromQuery] string phone)
		{
			var query = new GetAccountByPhoneQuery(phone);
			var account = await _mediator.Send(query);
			if (account == null)
			{
				return Ok(new { message = "Account not found." });
			}
			return Ok(account);
		}
	}
}
