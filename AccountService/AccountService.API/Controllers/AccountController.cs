using AccountService.API.Configuration;
using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AccountService.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAccountRepositoryRead _readRepo;
		private readonly IAccountRepositoryWrite _writeRepo;
		private readonly JwtTokenGenerator _jwtTokenGenerator;
		public AccountController(IAccountRepositoryRead readRepo,IAccountRepositoryWrite writeRepo,JwtTokenGenerator jwtTokenGenerator)
		{
			_readRepo = readRepo;
			_writeRepo = writeRepo;
			_jwtTokenGenerator = jwtTokenGenerator;
		}

		[HttpGet("login-google")]
		public IActionResult LoginWithGoogle()
		{
			var redirectUrl = Url.Action("GoogleResponse", "Account");
			var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
			return Challenge(properties, GoogleDefaults.AuthenticationScheme);
		}

		[HttpGet("google-response")]
		public async Task<IActionResult> GoogleResponse()
		{
			var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			if (!result.Succeeded) return Unauthorized();

			var email = result.Principal.FindFirst(ClaimTypes.Email)?.Value;
			var name = result.Principal.FindFirst(ClaimTypes.Name)?.Value;

			var account = await _readRepo.GetByEmailAsync(email!);

			if (account == null)
			{
				// Tạo object mới
				account = new Account
				{
					AccountFullName = name!,
					AccountUsername = name!,
					AccountEmail = email!,
					AccountPassword = "GOOGLE_LOGIN",
					AccountRole = Role.USER.ToString(),
					AccountStatus = Status.ACTIVE.ToString(),
				};

				// Lưu vào SQL Server (writeRepo)
				await _writeRepo.AddAsync(account);
				await _writeRepo.SaveChangesAsync();

				// Đồng bộ sang PostgreSQL (readRepo)
				var accountCopy = new Account
				{
					AccountId = account.AccountId, // giữ ID giống nhau nếu cần đồng bộ
					AccountFullName = account.AccountFullName,
					AccountUsername = account.AccountUsername,
					AccountEmail = account.AccountEmail,
					AccountPassword = account.AccountPassword,
					AccountRole = account.AccountRole,
					AccountStatus = account.AccountStatus,
				};

				await _readRepo.AddAsync(accountCopy);
				await _readRepo.SaveChangesAsync();
			}

			var token = _jwtTokenGenerator.GenerateToken(account.AccountEmail, account.AccountFullName);

			return Ok(new
			{
				message = "Login successful",
				token,
				user = new
				{
					account.AccountId,
					account.AccountFullName,
					account.AccountEmail,
				}
			});
		}
	}
}