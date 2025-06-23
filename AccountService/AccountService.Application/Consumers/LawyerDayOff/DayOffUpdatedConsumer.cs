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
	public class DayOffUpdatedConsumer : IConsumer<DayOffUpdatedEvent>
	{
		private readonly ILawyerDayOffScheduleRepositoryRead _repoRead;
		private readonly ISpecificDayOffRepositoryRead _specificDayOffRepositoryRead;

		public DayOffUpdatedConsumer(ILawyerDayOffScheduleRepositoryRead repoRead, ISpecificDayOffRepositoryRead specificDayOffRepositoryRead)
		{
			_repoRead = repoRead;
			_specificDayOffRepositoryRead = specificDayOffRepositoryRead;
		}

		public async Task Consume(ConsumeContext<DayOffUpdatedEvent> context)
		{
			var message = context.Message;
			var dayOff = new LawyerDayOffSchedule
			{
				LawyerDayOffScheduleId = message.LawyerScheduleDayOffId,
				OffDay = message.dayOff,
			};
			var specific = message.shiftId.Select(s => new SpecificLawyerDayOffSchedule
			{
				LawyerDayOffScheduleId = message.LawyerScheduleDayOffId,
				ShiftId = Guid.Parse(s),
				Status = ShiftStatus.WAITING.ToString()
			}).ToList();
			await _repoRead.UpdateLawyerDayOffScheduleAsync(dayOff);
			await _specificDayOffRepositoryRead.UpdateAsync(specific, message.LawyerScheduleDayOffId);
		}
	}
}
