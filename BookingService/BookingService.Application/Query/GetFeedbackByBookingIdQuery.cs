using BookingService.Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Query
{
	public record GetFeedbackByBookingIdQuery(Guid BookingId) : IRequest<FeedbackResponse>;

}
