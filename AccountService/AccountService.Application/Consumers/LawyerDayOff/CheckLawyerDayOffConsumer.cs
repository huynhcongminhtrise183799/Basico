using AccountService.Domain.IRepositories;
using Contracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Consumers.LawyerDayOff
{
	public class CheckLawyerDayOffConsumer : IConsumer<CheckLawyerDayOff>
	{
		private readonly IShiftRepositoryRead _repository;

		public CheckLawyerDayOffConsumer(IShiftRepositoryRead repository)
		{
            _repository = repository;
		}

		public async Task Consume(ConsumeContext<CheckLawyerDayOff> context)
		{
			Console.WriteLine("CheckLawyerDayOffConsumer: Checking lawyer's day off...");
			var message = context.Message;
			var shiftsOff = await _repository.GetShiftsOffByLawyerInDay(message.LawyerId, message.DayOffDate);
			await context.RespondAsync(new CheckLawyerDayOff
			{
				CorrelationId = message.CorrelationId,
				LawyerId = message.LawyerId,
				DayOffDate = message.DayOffDate,
				ShiftOffs = shiftsOff.Select(s => new ShiftOff
				{
					StartTime = s.StartTime,
					EndTime = s.EndTime,
				}).ToList()
			});
		}
	}
}
