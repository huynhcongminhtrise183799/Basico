using AccountService.Domain.IRepositories;
using Contracts.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Consumers.Form
{
	public class BuyFormSuccessConsumerService : IConsumer<BuyFormSuccessEvent>
	{
		private readonly IAccountRepositoryWrite _accountWriteRepo;
		private readonly IAccountRepositoryRead _accountReadRepo;
		public BuyFormSuccessConsumerService(
			IAccountRepositoryWrite accountWriteRepo,
			IAccountRepositoryRead accountReadRepo)
		{
			_accountWriteRepo = accountWriteRepo;
			_accountReadRepo = accountReadRepo;
		}
		public async Task Consume(ConsumeContext<BuyFormSuccessEvent> context)
		{
			var message = context.Message;
			var account = await _accountWriteRepo.GetByUserIdAsync(message.CustomerId, context.CancellationToken);
			if (account == null)
			{
				throw new Exception($"Account with UserId {message.CustomerId} not found.");
			}
			account.AccountTicketRequest -= message.Request;
			await _accountWriteRepo.UpdateAccount(account);
			await _accountReadRepo.UpdateAccount(account);
		}
	}
}
