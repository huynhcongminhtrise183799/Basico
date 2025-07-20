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

namespace AccountService.Application.Handler.CommandHandler.DayOff
{
	public class JustifyDayOffHandlerService : IRequestHandler<JustifyDayOffCommand, bool>
	{
		private readonly ISpecificDayOffRepositoryWrite _repoWrite;
		private readonly IPublishEndpoint _publish;
		public JustifyDayOffHandlerService(ISpecificDayOffRepositoryWrite repoWrite, IPublishEndpoint publish)
		{
			_repoWrite = repoWrite;
			_publish = publish;
		}
		public async Task<bool> Handle(JustifyDayOffCommand request, CancellationToken cancellationToken)
		{
			var specificList = request.requests.Select(x => new SpecificLawyerDayOffSchedule
			{
				LawyerDayOffScheduleId = request.lawyerIdScheduleId,
				ShiftId = x.ShiftId,
				Status = x.Status
			}).ToList();
			 var result = await _repoWrite.UpdateAsync(specificList, request.lawyerIdScheduleId);
			if (!result) { return false; } // Update failed
			var @event = new DayOffJustifiedEvent
			{
				requests = request.requests,
				lawyerIdScheduleId = request.lawyerIdScheduleId
			};
			await _publish.Publish(@event, cancellationToken);
			return true;

		}
	}

}
