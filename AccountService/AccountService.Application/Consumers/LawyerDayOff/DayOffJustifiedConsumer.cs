using AccountService.Application.Event.DayOff;
using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Consumers.LawyerDayOff
{
	public class DayOffJustifiedConsumer : IConsumer<DayOffJustifiedEvent>
	{
		private readonly ISpecificDayOffRepositoryRead _repoRead;

		public DayOffJustifiedConsumer(ISpecificDayOffRepositoryRead repoRead)
		{
			_repoRead = repoRead;
		}

		public async Task Consume(ConsumeContext<DayOffJustifiedEvent> context)
		{
			var message = context.Message;
			var specific = message.requests.Select(x => new SpecificLawyerDayOffSchedule
			{
				LawyerDayOffScheduleId = message.lawyerIdScheduleId,
				ShiftId = x.ShiftId,
				Status = x.Status
			}).ToList();
			await _repoRead.UpdateAsync(specific, message.lawyerIdScheduleId);
		}
	}
}
