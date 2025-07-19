using AccountService.Application.Commands.ForgotPasswordCommands;
using AccountService.Application.Event.ForgotPasswordEvent;
using AccountService.Domain.IRepositories;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.CommandHandler.ForgotPasswordCommandHandler
{
	public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
	{
		private readonly IAccountRepositoryWrite _repoWrite;
		private readonly IPublishEndpoint _publishEndpoint;

		public ResetPasswordCommandHandler(IAccountRepositoryWrite repoWrite, IPublishEndpoint publishEndpoint)
		{
			_repoWrite = repoWrite;
			_publishEndpoint = publishEndpoint;
		}


		public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
		{
			var passwordHasher = new PasswordHasher<string>();
			var hashedPassword = passwordHasher.HashPassword(null, request.NewPassword);
			var result = await _repoWrite.ResetPasswordAsync(request.Email, hashedPassword);
			if (!result)
			{
				return false; // Reset password failed
			}
			await _publishEndpoint.Publish(new ResetPasswordEvent(request.Email, hashedPassword));
			return true; // Reset password successful
		}
	}
}
