using AccountService.Application.Event.StaffEvent;
using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Consumers.StaffConsumers
{
	public class StaffCreatedEventConsumersService : IConsumer<StaffCreatedEvent>
	{
		private readonly IAccountRepositoryRead _repo;

		public StaffCreatedEventConsumersService(IAccountRepositoryRead repo)
		{
			_repo = repo;
		}

		public async Task Consume(ConsumeContext<StaffCreatedEvent> context)
		{
			var message = context.Message;
			var staff = await _repo.GetAccountById(message.StaffId);
			if (staff != null)
			{
				Console.WriteLine($"Staff with ID {message.StaffId} already exists.");
				return;
			}
			Account account = new Account
			{
				AccountId = message.StaffId,
				AccountEmail = message.Email,
				AccountFullName = message.FullName,
				AccountPassword = message.Password,
				AccountUsername = message.Username,
				AccountGender = message.Gender,
				AccountImage = message.ImageUrl,
			};
			await _repo.AddStaff(account);
			Console.WriteLine("Save Postgres successfully");
		}
	}
}
