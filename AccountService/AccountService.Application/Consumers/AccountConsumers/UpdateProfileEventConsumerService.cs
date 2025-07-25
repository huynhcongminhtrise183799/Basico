﻿using AccountService.Application.Event.AccountEvent;
using AccountService.Domain.IRepositories;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Consumers.AccountConsumers
{
	public class UpdateProfileEventConsumerService  : IConsumer<UpdatedProfileEvent>
	{
		private readonly IAccountRepositoryRead _repo;

		public UpdateProfileEventConsumerService(IAccountRepositoryRead repo)
		{
			_repo = repo;
		}

		public async Task Consume(ConsumeContext<UpdatedProfileEvent> context)
		{
			var message = context.Message;
			var account = await _repo.GetAccountById(message.AccountId);
			if (account != null)
			{
				account.AccountFullName = message.AccountFullName;
				account.AccountGender = message.AccountGender;
				account.AccountImage = message.AccountImage;
				await _repo.UpdateAccount(account);
				Console.WriteLine("Profile updated successfully");
			}
			else
			{
				Console.WriteLine("Account not found");
			}
		}
	}
}
