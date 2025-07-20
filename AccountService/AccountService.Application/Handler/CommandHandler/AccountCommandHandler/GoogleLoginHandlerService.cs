using AccountService.Application.Commands.AccountCommands;
using AccountService.Application.DTOs.Response;
using AccountService.Application.Event;
using AccountService.Application.Event.AccountEvent;
using AccountService.Application.IService;
using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.CommandHandler.AccountHandler
{
	public class GoogleLoginHandlerService : IRequestHandler<GoogleLoginCommand, GoogleLoginResponse>
	{
		private readonly IAccountRepositoryRead _readRepo;
		private readonly IAccountRepositoryWrite _writeRepo;
		private readonly ITokenService _tokenService;
		private readonly IPublishEndpoint _publishEndpoint;


		public GoogleLoginHandlerService(IAccountRepositoryRead readRepo, IAccountRepositoryWrite writeRepo, ITokenService tokenService, IPublishEndpoint publishEndpoint)
		{
			_readRepo = readRepo;
			_writeRepo = writeRepo;
			_tokenService = tokenService;
			_publishEndpoint = publishEndpoint;
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
			    await _publishEndpoint.Publish(new GoogleAccountCreatedEvent(account.AccountId,account.AccountFullName, account.AccountUsername, account.AccountEmail, account.AccountPassword, account.AccountRole, account.AccountStatus));
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
