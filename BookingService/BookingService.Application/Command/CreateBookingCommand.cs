﻿using BookingService.Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Command
{
	public record CreateBookingCommand(DateOnly bookingDate,string Description, double Price, Guid? CustomerId, Guid lawyerId, Guid ServiceId, List<string> SlotId) : IRequest<BookingResponse>;

}
