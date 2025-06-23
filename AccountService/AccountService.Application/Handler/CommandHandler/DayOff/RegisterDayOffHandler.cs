using AccountService.Application.Commands.DayOff;
using AccountService.Application.DTOs.Response;
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

namespace AccountService.Application.Handler.CommandHandler.DayOff
{
	public class RegisterDayOffHandler : IRequestHandler<RegisterDayOffCommand, RegisterDayOffResponse>
	{
		private readonly ILawyerDayOffScheduleRepositoryWrite _repoWrite;
		private readonly ISpecificDayOffRepositoryWrite _specificWrite;
		private readonly IPublishEndpoint _publish;
		public RegisterDayOffHandler(ILawyerDayOffScheduleRepositoryWrite repoWrite, ISpecificDayOffRepositoryWrite specificWrite, IPublishEndpoint publish)
		{
			_repoWrite = repoWrite;
			_specificWrite = specificWrite;
			_publish = publish;
		}
		public async Task<RegisterDayOffResponse> Handle(RegisterDayOffCommand request, CancellationToken cancellationToken)
		{
			var lawyerDayOffSchedule = new LawyerDayOffSchedule
			{
				LawyerDayOffScheduleId = Guid.NewGuid(),
				LawyerId = request.lawyerId,
				OffDay = request.dayOff,
			};
			var specificDayOff = request.shiftId.Select(shiftId => new SpecificLawyerDayOffSchedule
			{
				LawyerDayOffScheduleId = lawyerDayOffSchedule.LawyerDayOffScheduleId,
				ShiftId = Guid.Parse(shiftId),
				Status = ShiftStatus.WAITING.ToString()
			}).ToList();
			await _repoWrite.AddLawyerDayOffScheduleAsync(lawyerDayOffSchedule);
			await _specificWrite.AddAsync(specificDayOff);
			var @event = new DayOffCreatedEvent
			{
				LawyerScheduleDayOffId = lawyerDayOffSchedule.LawyerDayOffScheduleId,
				LawyerId = request.lawyerId,
				dayOff = request.dayOff,
				shiftId = request.shiftId
			};
			await _publish.Publish(@event, cancellationToken);
			return new RegisterDayOffResponse
			{
				DayOffId = lawyerDayOffSchedule.LawyerDayOffScheduleId,
				AccountId = request.lawyerId,
				DayOff = request.dayOff,
				specificLawyerDayOffSchedules = specificDayOff
			};


		}
	}
}
