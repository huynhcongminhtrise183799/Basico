using BookingService.Application.DTOs.Response;
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
	public class GetBookingByCustomerAndStatusHandlerService : IRequestHandler<GetBookingByCustomerAndStatusQuery, List<BookingDetailResponse>>
	{
		private readonly IBookingRepositoryRead _bookingRepository;
		private readonly ISlotRepositoryRead _slotRepository;
        private readonly IClientFactory _clientFactory;
        public GetBookingByCustomerAndStatusHandlerService(IBookingRepositoryRead bookingRepository, ISlotRepositoryRead slotRepository, IClientFactory clientFactory)
		{
			_bookingRepository = bookingRepository;
            _slotRepository = slotRepository;
            _clientFactory = clientFactory;
		}
		public async Task<List<BookingDetailResponse>> Handle(GetBookingByCustomerAndStatusQuery request, CancellationToken cancellationToken)
		{
			var bookings = await _bookingRepository.GetBookingsByCusomterIdAndStatus(request.customerId, request.status);
			var bookingDetailResponses = new List<BookingDetailResponse>();
			if (bookings.Count > 0)
			{
				foreach(var b in bookings)
				{
					var slots = await _slotRepository.GetSlotsByBookingId(b.BookingId);
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
						Description = b.Description,
						LawyerId = b.LawyerId,
						ServiceId = b.ServiceId,
						CustomerId = b.CustomerId,
						Status = b.Status,
						StartTime = slots.Min(slot => slot.SlotStartTime),
						EndTime = slots.Max(slot => slot.SlotEndTime),

					};
						bookingDetailResponses.Add(bookingDetailResponse);
					
				}
				return bookingDetailResponses;
			}
			return null;
		}
	}
}
