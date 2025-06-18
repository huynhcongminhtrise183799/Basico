using BookingService.Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Query
{
	public record GetBookingByLawyerAndStatusQuery(Guid LawyerId, string Status, DateOnly BookingDate) : IRequest<List<BookingDetailResponse>>;


}
