using AccountService.Application.Commands.DayOff;
using AccountService.Application.Event.DayOff;
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
	public class DeleteDayOffHandler : IRequestHandler<DeleteDayOffCommand, bool>
	{
		private readonly ILawyerDayOffScheduleRepositoryWrite _repoWrite;
		private readonly ISpecificDayOffRepositoryWrite _specificDayOffRepositoryWrite;
		private readonly IPublishEndpoint _publish;
		public DeleteDayOffHandler(ILawyerDayOffScheduleRepositoryWrite repoWrite, ISpecificDayOffRepositoryWrite specificDayOffRepositoryWrite, IPublishEndpoint publish)
		{
			_repoWrite = repoWrite;
			_specificDayOffRepositoryWrite = specificDayOffRepositoryWrite;
			_publish = publish;
		}
		public async Task<bool> Handle(DeleteDayOffCommand request, CancellationToken cancellationToken)
		{
			await _specificDayOffRepositoryWrite.DeleteAsync(request.lawyerDayOffScheduleId);
			await _repoWrite.DeleteLawyerDayOffScheduleAsync(request.lawyerDayOffScheduleId);
			var @event = new DayOffDeletedEvent
			{
				LawyerDayOffScheduleId = request.lawyerDayOffScheduleId
			};
			await _publish.Publish(@event, cancellationToken);
			return true;
		}
	}

}
