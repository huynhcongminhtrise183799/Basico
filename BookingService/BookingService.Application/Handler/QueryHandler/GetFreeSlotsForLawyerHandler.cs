using BookingService.Application.Message;
using BookingService.Application.Query;
using BookingService.Domain.Entities;
using BookingService.Domain.IRepository;
using Contracts;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Handler.QueryHandler
{
	public class GetFreeSlotsForLawyerHandler : IRequestHandler<GetFreeSlotsForLawyerQuery, List<Slot>>
	{
		private readonly ISlotRepositoryRead _slotRepositoryRead;
		//private readonly IEventPublisher _publishEndpoint;
		private readonly IClientFactory _clientFactory;

		public GetFreeSlotsForLawyerHandler(ISlotRepositoryRead slotRepositoryRead, IClientFactory publishEndpoint)
		{
			_slotRepositoryRead = slotRepositoryRead;
            _clientFactory = publishEndpoint;
		}
		public async Task<List<Slot>> Handle(GetFreeSlotsForLawyerQuery request, CancellationToken cancellationToken)
		{
			var checkDayOff = new CheckLawyerDayOff
			{
				CorrelationId = Guid.NewGuid(),
				LawyerId = request.lawyerId,
				DayOffDate = request.DateOnly
			};
			var client = _clientFactory.CreateRequestClient<CheckLawyerDayOff>();
			// Send the request and wait for the response
			var response = await client.GetResponse<CheckLawyerDayOff>(checkDayOff, cancellationToken, timeout: RequestTimeout.After(s: 60));
			var allSlots = await _slotRepositoryRead.GetAllSlots();
			var currentTime = TimeOnly.FromDateTime(DateTime.Now);
			var currentDay = DateOnly.FromDateTime(DateTime.Now);
			if(request.DateOnly == currentDay)
			{
				allSlots = allSlots.Where(slot => slot.SlotStartTime >= currentTime).ToList();

			}
			var busySlots = await _slotRepositoryRead.GetBusySlots(request.lawyerId, request.DateOnly);
			var freeSlots = allSlots.Where(slot => !busySlots.Any(busySlot => busySlot.SlotId == slot.SlotId)).ToList();
			if (response.Message.ShiftOffs != null)
			{
				foreach(var shift in response.Message.ShiftOffs)
				{
					freeSlots = freeSlots
						.Where(slot => slot.SlotStartTime < shift.StartTime || slot.SlotEndTime > shift.EndTime)
						.ToList();
				}
			}
			return freeSlots;
		}
	}
}
