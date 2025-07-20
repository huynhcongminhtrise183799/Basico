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
	public class BanAccountConsumerService : IConsumer<BanAccountEvent>
	{
		private readonly IAccountRepositoryRead _repoRead;

		public BanAccountConsumerService(IAccountRepositoryRead repoRead)
		{
			_repoRead = repoRead;
		}

		public async Task Consume(ConsumeContext<BanAccountEvent> context)
		{
			var accountId = context.Message.AccountId;
			await _repoRead.BanUserAccount(accountId);
		}
	}

}
