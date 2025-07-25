﻿using BookingService.Application.DTOs.Response;
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
	public class GetBookingByIdHandlerService : IRequestHandler<GetBookingByIdQuery, BookingDetailResponse>
	{
		private readonly IBookingRepositoryRead _bookingRepository;
		private readonly ISlotRepositoryRead _slot;
		private readonly IEventPublisher _eventPublisher;
		public GetBookingByIdHandlerService(IBookingRepositoryRead bookingRepository, ISlotRepositoryRead slot, IEventPublisher eventPublisher)
		{
			_bookingRepository = bookingRepository;
			_slot = slot;
			_eventPublisher = eventPublisher;
		}
		public async Task<BookingDetailResponse> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
		{
			var booking = await _bookingRepository.GetBookingByIdAsync(request.bookingId);
			var slots = await _slot.GetSlotsByBookingId(request.bookingId);
			var findLawyerName = new GetDetailBookingInformation
			{
				CorrelationId = Guid.NewGuid(),
				LawyerId = booking.LawyerId,
				ServiceId = booking.ServiceId,
				CustomerId = booking.CustomerId
			};
			var client = _eventPublisher.CreateRequestClient<GetDetailBookingInformation>();
			var response = await client.GetResponse<GetDetailBookingInformation>(findLawyerName, cancellationToken, timeout: RequestTimeout.After(s: 60));
			var bookingDetailResponse = new BookingDetailResponse
			{
				BookingId = booking.BookingId,
				BookingDate = booking.BookingDate,
				Price = booking.Price,
				LawyerName = response.Message.LawyerName,
				ServiceName = response.Message.ServiceName,
				Status = booking.Status,
				StartTime = slots.Min(slot => slot.SlotStartTime),
				Description = booking.Description,
				EndTime = slots.Max(slot => slot.SlotEndTime),
				CustomerName = response.Message.CustomerName,
				CustomerId = booking.CustomerId,
				LawyerId = booking.LawyerId,
				ServiceId = booking.ServiceId
			};
			return bookingDetailResponse;
		}
	}
}
