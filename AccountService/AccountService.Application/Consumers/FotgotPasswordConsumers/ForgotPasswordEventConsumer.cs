using AccountService.Application.Event.ForgotPasswordEvent;
using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Consumers.FotgotPasswordConsumers
{
	public class ForgotPasswordEventConsumer : IConsumer<ForgotPasswordEvent>
	{
		private readonly IAccountRepositoryRead _repoRead;

		public ForgotPasswordEventConsumer(IAccountRepositoryRead repoRead)
		{
			_repoRead = repoRead;
		}
		public async Task Consume(ConsumeContext<ForgotPasswordEvent> context)
		{
			var message = context.Message;
			var expirationTimeUtc = message.ExpirationDate.UtcDateTime;
			await _repoRead.SaveOtpAsync(message.AccountId, message.Otp, expirationTimeUtc, message.ForgetPasswordId);
			Console.WriteLine("Save Postgre successfully");
		}
	}
}
