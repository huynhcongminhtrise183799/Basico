using BookingService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Query
{
	public record GetFreeSlotsForUpdateQuery(Guid bookingId ,Guid lawyerId, DateOnly bookingDate) : IRequest<List<Slot>>;

}
