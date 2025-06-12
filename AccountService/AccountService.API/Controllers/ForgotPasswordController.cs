using AccountService.Application.Commands.ForgotPasswordCommands;
using AccountService.Application.DTOs.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AccountService.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ForgotPasswordController : Controller
	{
		private readonly IMediator _mediator;
		private readonly IMemoryCache _memoryCache;
		public ForgotPasswordController(IMediator mediator, IMemoryCache memoryCache)
		{
			_mediator = mediator;
			_memoryCache = memoryCache;
		}
		[HttpPost("request")]
		public async Task<IActionResult> RequestForgotPassword([FromBody] ForgotPasswordRequest request)
		{
			var command = new ForgotPasswordRequestCommand(request.Email);
			var result = await _mediator.Send(command);
			if (!result)
				return BadRequest(new { message = "Failed to send OTP" });
			return Ok(new { message = "OTP sent to your email" });
		}

		[HttpPost("verify")]
		public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequest request)
		{
			var command = new VerifyOtpCommand(request.Email, request.OTP);
			var result = await _mediator.Send(command);
			if (!result)
				return BadRequest(new { message = "OTP invalid or expired" });

			// Lưu trạng thái đã xác minh OTP, tồn tại trong 5 phút
			_memoryCache.Set($"otp_verified_{request.Email}", true, TimeSpan.FromMinutes(5));

			return Ok(new { message = "OTP verified successfully" });
		}


		[HttpPost("reset")]
		public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
		{
			if (!_memoryCache.TryGetValue($"otp_verified_{request.Email}", out bool isVerified) || !isVerified)
			{
				return BadRequest(new { message = "OTP not verified" });
			}

			var command = new ResetPasswordCommand(request.Email, request.NewPassword);
			var result = await _mediator.Send(command);

			if (result)
			{
				_memoryCache.Remove($"otp_verified_{request.Email}");
				return Ok(new { message = "Password reset successfully" });
			}
			else
			{
				return BadRequest(new { message = "Failed to reset password" });
			}
		}
	}
}