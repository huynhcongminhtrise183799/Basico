using AccountService.Application.Event.AccountEvent;
using AccountService.Domain.IRepositories;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Consumers.AccountConsumers
{
	public class ActiveAccountConsumer : IConsumer<ActiveAccountEvent>
	{
		private readonly IAccountRepositoryRead _repoRead;
		public ActiveAccountConsumer(IAccountRepositoryRead repoRead)
		{
			_repoRead = repoRead;
		}
		public async Task Consume(ConsumeContext<ActiveAccountEvent> context)
		{
			var accountId = context.Message.AccountId;
			await _repoRead.ActiveUserAccount(accountId);
		}
	}

}
