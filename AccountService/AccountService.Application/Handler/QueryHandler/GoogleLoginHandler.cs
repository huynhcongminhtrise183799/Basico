
using AccountService.Application.Commands;
using AccountService.Application.DTOs.Response;
using AccountService.Application.IService;
using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.QueryHandler
{
	public class GoogleLoginHandler : IRequestHandler<GoogleLoginCommand, GoogleLoginResponse>
	{
		private readonly IAccountRepositoryRead _readRepo;
		private readonly IAccountRepositoryWrite _writeRepo;
		private readonly ITokenService _tokenService;

		public GoogleLoginHandler(IAccountRepositoryRead readRepo, IAccountRepositoryWrite writeRepo, ITokenService tokenService)
		{
			_readRepo = readRepo;
			_writeRepo = writeRepo;
			_tokenService = tokenService;
		}

		public async Task<GoogleLoginResponse> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
		{
			var account = await _readRepo.GetByEmailAsync(request.Email);

			if (account == null)
			{
				account = new Account
				{
					AccountFullName = request.FullName,
					AccountUsername = request.FullName,
					AccountEmail = request.Email,
					AccountPassword = "GOOGLE_LOGIN",
					AccountRole = Role.USER.ToString(),
					AccountStatus = Status.ACTIVE.ToString(),
				};

				await _writeRepo.AddAsync(account);
				await _writeRepo.SaveChangesAsync();

				var accountCopy = new Account
				{
					AccountId = account.AccountId,
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

			var token = _tokenService.GenerateToken(account);

			return new GoogleLoginResponse
			{
				Message = "Login successful",
				Token = token,
				AccountId = account.AccountId,
				FullName = account.AccountFullName,
				Email = account.AccountEmail
			};
		}
	}
}
