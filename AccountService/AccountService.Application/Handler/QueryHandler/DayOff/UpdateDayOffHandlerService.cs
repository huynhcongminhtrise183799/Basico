using AccountService.Application.Commands.DayOff;
using AccountService.Application.Event.DayOff;
using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.QueryHandler.DayOff
{
	public class UpdateDayOffHandlerService : IRequestHandler<UpdateDayOffCommand, bool>
	{
		private readonly ILawyerDayOffScheduleRepositoryWrite _repoWrite;
		private readonly ISpecificDayOffRepositoryWrite _specificDayOffRepositoryWrite;
		private readonly IPublishEndpoint _publish;

		public UpdateDayOffHandlerService(ILawyerDayOffScheduleRepositoryWrite repoWrite, ISpecificDayOffRepositoryWrite specificDayOffRepositoryWrite, IPublishEndpoint publish)
		{
			_repoWrite = repoWrite;
			_specificDayOffRepositoryWrite = specificDayOffRepositoryWrite;
			_publish = publish;
		}

		public async Task<bool> Handle(UpdateDayOffCommand request, CancellationToken cancellationToken)
		{
			var dayOff = new LawyerDayOffSchedule
			{
				LawyerDayOffScheduleId = request.lawyerDayOffScheduleId,
				OffDay = request.dayOff
			};
			var specific = request.shiftId.Select(shiftId => new SpecificLawyerDayOffSchedule
			{
				LawyerDayOffScheduleId = request.lawyerDayOffScheduleId,
				ShiftId = Guid.Parse(shiftId),
				Status = ShiftStatus.WAITING.ToString()
			}).ToList();
			await _repoWrite.UpdateLawyerDayOffScheduleAsync(dayOff);
			await _specificDayOffRepositoryWrite.UpdateAsync(specific,request.lawyerDayOffScheduleId);
			var @event = new DayOffUpdatedEvent
			{
				LawyerScheduleDayOffId = request.lawyerDayOffScheduleId,
				dayOff = request.dayOff,
				shiftId = request.shiftId
			};
			await _publish.Publish(@event, cancellationToken);
			return true;
		}
	}
}
