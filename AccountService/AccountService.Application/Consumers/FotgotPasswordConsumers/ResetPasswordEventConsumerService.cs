using AccountService.Application.Event.ForgotPasswordEvent;
using AccountService.Domain.IRepositories;
using MassTransit;
using System.Threading.Tasks;

namespace AccountService.Application.Consumers.ForgotPasswordConsumers
{
	public class ResetPasswordEventConsumerService : IConsumer<ResetPasswordEvent>
	{
		private readonly IAccountRepositoryRead _repoRead;

		public ResetPasswordEventConsumerService(IAccountRepositoryRead repoRead)
		{
			_repoRead = repoRead;
		}

		public async Task Consume(ConsumeContext<ResetPasswordEvent> context)
		{
			var message = context.Message;
			var account = await _repoRead.GetByEmailAsync(message.Email);
			if (account != null)
			{
				account.AccountPassword = message.Password;
				await _repoRead.UpdateAccount(account);
				Console.WriteLine("Profile updated successfully");
			}
			else
			{
				Console.WriteLine("Account not found");
			}
		}
	}
}
