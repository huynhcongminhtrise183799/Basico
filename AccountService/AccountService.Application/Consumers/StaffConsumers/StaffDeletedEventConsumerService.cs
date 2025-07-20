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
	public class StaffDeletedEventConsumerService : IConsumer<StaffDeletedEvent>
	{
		private readonly IAccountRepositoryRead _repo;

		public StaffDeletedEventConsumerService(IAccountRepositoryRead repo)
		{
			_repo = repo;
		}
		public async Task Consume(ConsumeContext<StaffDeletedEvent> context)
		{
			// Xử lý logic khi sự kiện StaffDeletedEvent được nhận
			var staffId = context.Message.StaffId;
			await _repo.DeleteStaff(staffId);
			Console.WriteLine($"Staff with ID {staffId} has been deleted.");
		}
	}
}
