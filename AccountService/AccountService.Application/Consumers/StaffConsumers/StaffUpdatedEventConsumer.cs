using AccountService.Application.Event.StaffEvent;
using AccountService.Domain.IRepositories;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Consumers.StaffConsumers
{
	public class StaffUpdatedEventConsumer : IConsumer<StaffUpdateEvent>
	{
		private readonly IAccountRepositoryRead _repo;

		public StaffUpdatedEventConsumer(IAccountRepositoryRead repo)
		{
			_repo = repo;
		}
		public async Task Consume(ConsumeContext<StaffUpdateEvent> context)
		{
			var message = context.Message;
			var account = await _repo.GetAccountById(message.AccountId);
			if (account != null)
			{
				account.AccountFullName = message.FullName;
				account.AccountGender = message.Gender;
				account.AccountStatus = message.Status.ToString();
				account.AccountImage = message.ImageUrl;
				await _repo.UpdateStaff(account);
				Console.WriteLine("Profile updated successfully");
			}
			else
			{
				Console.WriteLine("Account not found");
			}
		}
	}
}
