using BookingService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Query
{
	public record GetFreeSlotsForLawyerQuery(Guid lawyerId, DateOnly DateOnly) : IRequest<List<Slot>>;

}
