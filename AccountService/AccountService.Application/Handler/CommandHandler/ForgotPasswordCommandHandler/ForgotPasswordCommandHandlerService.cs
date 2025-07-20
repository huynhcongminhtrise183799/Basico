using AccountService.Application.Commands.ForgotPasswordCommands;
using AccountService.Application.Event.ForgotPasswordEvent;
using AccountService.Application.IService;
using AccountService.Domain.IRepositories;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.CommandHandler.ForgotPasswordCommandHandler
{
	public class ForgotPasswordCommandHandlerService : IRequestHandler<ForgotPasswordRequestCommand, bool>
	{
		private readonly IAccountRepositoryWrite _repoWrite;
		private readonly IEmailService _emailService;
		private readonly IPublishEndpoint _publishEndpoint;
		private readonly IAccountRepositoryRead _repoRead;

		public ForgotPasswordCommandHandlerService(IAccountRepositoryWrite repoWrite, IEmailService emailService, IPublishEndpoint publishEndpoint, IAccountRepositoryRead repoRead)
		{
			_repoWrite = repoWrite;
			_emailService = emailService;
			_publishEndpoint = publishEndpoint;
			_repoRead = repoRead;
		}
		public async Task<bool> Handle(ForgotPasswordRequestCommand request, CancellationToken cancellationToken)
		{
			var account = await _repoWrite.FindByEmailAsync(request.Email);
			if (account == null) return false;
			var expirationTimeOffset = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(7)).AddMinutes(5);
			var expirationTime = expirationTimeOffset.UtcDateTime;
			var otp = new Random().Next(100000, 999999).ToString();
			var forgotPassword = await _repoWrite.SaveOtpAsync(account.AccountId, otp, expirationTimeOffset.DateTime);
			var forgotPasswordId = forgotPassword.ForgotPasswordId;
			//await _repoRead.SaveOtpAsync(account.AccountId, otp, expirationTime, forgotPasswordId);
			await _emailService.SendAsync(request.Email, "Your OTP", $"Your OTP is {otp}");
			await _publishEndpoint.Publish(new ForgotPasswordEvent(
				account.AccountId, // hoặc dùng trực tiếp nếu đã là Guid
				otp,
				expirationTime,
				forgotPasswordId
			));
			return true;
		}
	}
}
