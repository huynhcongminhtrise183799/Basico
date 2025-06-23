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
	public class DayOffCreatedConsumer : IConsumer<DayOffCreatedEvent>
	{
		private readonly ILawyerDayOffScheduleRepositoryRead _repoRead;
		private readonly ISpecificDayOffRepositoryRead _specificDayOffRepositoryRead;
		public DayOffCreatedConsumer(ILawyerDayOffScheduleRepositoryRead repoRead, ISpecificDayOffRepositoryRead specificDayOffRepositoryRead)
		{
			_repoRead = repoRead;
			_specificDayOffRepositoryRead = specificDayOffRepositoryRead;
		}
		public async Task Consume(ConsumeContext<DayOffCreatedEvent> context)
		{
			var message = context.Message;
			var lawyerDayOff = new LawyerDayOffSchedule
			{
				LawyerDayOffScheduleId = message.LawyerScheduleDayOffId,
				LawyerId = message.LawyerId,
				OffDay = message.dayOff,
			};
			var specificLawyerDayOffSchedules = message.shiftId.Select(shiftId => new SpecificLawyerDayOffSchedule
			{
				LawyerDayOffScheduleId = lawyerDayOff.LawyerDayOffScheduleId,
				ShiftId = Guid.Parse(shiftId),
				Status = ShiftStatus.WAITING.ToString(),
			}).ToList();
			await _repoRead.AddLawyerDayOffScheduleAsync(lawyerDayOff);
			await _specificDayOffRepositoryRead.AddAsync(specificLawyerDayOffSchedules);
		}
	}
}
