using AccountService.Application.Event.AccountEvent;
using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Consumers.AccountConsumers
{
	public class GoogleAccountCreatedConsumersService : IConsumer<GoogleAccountCreatedEvent>
	{
		private readonly IAccountRepositoryRead _repo;

		public GoogleAccountCreatedConsumersService(IAccountRepositoryRead repo)
		{
			_repo = repo;
		}
		public async Task Consume(ConsumeContext<GoogleAccountCreatedEvent> context)
		{
			var message = context.Message;
			Account account = new Account
			{
				AccountId = message.AccountId,
				AccountFullName = message.AccountFullName,
				AccountUsername = message.AccountUsername,
				AccountEmail = message.AccountEmail,
				AccountPassword = "GOOGLE_LOGIN",
				AccountRole = Role.USER.ToString(),
				AccountStatus = Status.ACTIVE.ToString(),
			};
			await _repo.AddAsync(account);
			Console.WriteLine("Save Postgres successfully");
		}
	}
}
