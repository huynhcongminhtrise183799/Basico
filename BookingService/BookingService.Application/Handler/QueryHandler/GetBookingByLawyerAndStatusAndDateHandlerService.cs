using BookingService.Application.DTOs.Response;
using BookingService.Application.Message;
using BookingService.Application.Query;
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
	public class GetBookingByLawyerAndStatusAndDateHandlerService : IRequestHandler<GetBookingByLawyerAndStatusAndDateQuery, List<BookingDetailResponse>>
	{
		private readonly IBookingRepositoryRead _bookingRepository;
		private readonly ISlotRepositoryRead _slot;
		//private readonly IEventPublisher _eventPublisher;
		private readonly IClientFactory _clientFactory;
		public GetBookingByLawyerAndStatusAndDateHandlerService(IBookingRepositoryRead bookingRepository, ISlotRepositoryRead slot, IClientFactory clientFactory)
		{
			_bookingRepository = bookingRepository;
			_slot = slot;
            _clientFactory = clientFactory;
		}
		public async Task<List<BookingDetailResponse>> Handle(GetBookingByLawyerAndStatusAndDateQuery request, CancellationToken cancellationToken)
		{
			var bookings = await _bookingRepository.GetBookingsByLawyerIdAndStatusAndDate(request.LawyerId, request.Status,request.BookingDate);
			var bookingDetailResponses = new List<BookingDetailResponse>();
			if (bookings.Count > 0)
			{
				foreach (var b in bookings)
				{
					var slots = await _slot.GetSlotsByBookingId(b.BookingId);
					var findLawyerName = new GetDetailBookingInformation
					{
						CorrelationId = Guid.NewGuid(),
						LawyerId = b.LawyerId,
						ServiceId = b.ServiceId,
						CustomerId = b.CustomerId
					};
					var client = _clientFactory.CreateRequestClient<GetDetailBookingInformation>();
					var response = await client.GetResponse<GetDetailBookingInformation>(findLawyerName, cancellationToken, timeout: RequestTimeout.After(s: 60));
					var bookingDetailResponse = new BookingDetailResponse
					{
						BookingId = b.BookingId,
						BookingDate = b.BookingDate,
						Price = b.Price,
						LawyerName = response.Message.LawyerName,
						ServiceName = response.Message.ServiceName,
						CustomerName = response.Message.CustomerName,
						LawyerId = b.LawyerId,
						ServiceId = b.ServiceId,
						Description = b.Description,
						CustomerId = b.CustomerId,
						Status = b.Status,
						StartTime = slots.Min(slot => slot.SlotStartTime),
						EndTime = slots.Max(slot => slot.SlotEndTime)
					};
					bookingDetailResponses.Add(bookingDetailResponse);

				}
				return bookingDetailResponses;
			}
			return null;
		}
	}
}
