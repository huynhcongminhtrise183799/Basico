using BookingService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Domain.IRepository
{
	public interface IBookingRepositoryWrite
	{
		Task CreateBookingAsync(Booking booking);

		Task UpdateBookingAsync(Booking booking);

		Task DeleteBookingAsync(Guid bookingId);

		Task<bool> CheckInBookingAsync(Guid bookingId);

		Task UpdateStatusBookingToPaid(Guid? bookingId);

		Task UpdateStatusBookingToCompleted(Guid? bookingId);
	}
}
