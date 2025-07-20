using AccountService.Application.Event.DayOff;
using AccountService.Domain.IRepositories;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Consumers.LawyerDayOff
{
	public class DayOffDeletedConsumerService : IConsumer<DayOffDeletedEvent>
	{
		private readonly ILawyerDayOffScheduleRepositoryRead _repoRead;
		private readonly ISpecificDayOffRepositoryRead _specificDayOffRepositoryRead;

		public DayOffDeletedConsumerService(ILawyerDayOffScheduleRepositoryRead repoRead, ISpecificDayOffRepositoryRead specificDayOffRepositoryRead)
		{
			_repoRead = repoRead;
			_specificDayOffRepositoryRead = specificDayOffRepositoryRead;
		}

		public async Task Consume(ConsumeContext<DayOffDeletedEvent> context)
		{
			var message = context.Message;
			await _specificDayOffRepositoryRead.DeleteAsync(message.LawyerDayOffScheduleId);
			await _repoRead.DeleteLawyerDayOffScheduleAsync(message.LawyerDayOffScheduleId);
		}
	}

}
