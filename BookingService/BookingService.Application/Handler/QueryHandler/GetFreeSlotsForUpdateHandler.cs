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
	public class GetFreeSlotsForUpdateHandler : IRequestHandler<GetFreeSlotsForUpdateQuery, List<Slot>>
	{
		private readonly ISlotRepositoryRead _slotRepositoryRead;
		//private readonly IEventPublisher _publishEndpoint;
		private readonly IClientFactory _clientFactory;

		public GetFreeSlotsForUpdateHandler(ISlotRepositoryRead slotRepositoryRead, IClientFactory clientFactory)
		{
			_slotRepositoryRead = slotRepositoryRead;
            _clientFactory = clientFactory;
		}

		public async Task<List<Slot>> Handle(GetFreeSlotsForUpdateQuery request, CancellationToken cancellationToken)
		{
			var checkDayOff = new CheckLawyerDayOff
			{
				CorrelationId = Guid.NewGuid(),
				LawyerId = request.LawyerId,
				DayOffDate = request.BookingDate
			};
			var client = _clientFactory.CreateRequestClient<CheckLawyerDayOff>();
			// Send the request and wait for the response
			Console.WriteLine($"Requesting day off for lawyer {request.LawyerId} on {request.BookingDate}");
			var response = await client.GetResponse<CheckLawyerDayOff>(checkDayOff, cancellationToken, timeout: RequestTimeout.After(s: 60));
			Console.WriteLine($"Received response for day off: {response.Message.CorrelationId} - {response.Message.ShiftOffs?.Count ?? 0} shifts off");
			var allSlots = await _slotRepositoryRead.GetAllSlots();
			var currentTime = TimeOnly.FromDateTime(DateTime.Now);
			var currentDay = DateOnly.FromDateTime(DateTime.Now);
			if (request.BookingDate == currentDay)
			{
				allSlots = allSlots.Where(slot => slot.SlotStartTime >= currentTime).ToList();

			}
			var busySlots = await _slotRepositoryRead.GetBusySlotsForUpdate(request.BookingId,request.LawyerId, request.BookingDate);
			var freeSlots = allSlots.Where(slot => !busySlots.Any(busySlot => busySlot.SlotId == slot.SlotId)).ToList();
			if (response.Message.ShiftOffs != null)
			{
				foreach (var shift in response.Message.ShiftOffs)
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
