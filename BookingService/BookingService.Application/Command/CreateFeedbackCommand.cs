using BookingService.Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Command
{
	public record CreateFeedbackCommand(Guid BookingId, Guid CustomerId, string FeedbackContent, int Rating) : IRequest<FeedbackResponse>;

}
