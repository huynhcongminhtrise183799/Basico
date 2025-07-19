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
		Task<bool> CreateBookingAsync(Booking booking);

		Task<bool> UpdateBookingAsync(Booking booking);

		Task<bool> DeleteBookingAsync(Guid bookingId);

		Task<bool> CheckInBookingAsync(Guid bookingId);

		Task UpdateStatusBookingToPaid(Guid? bookingId);

		Task<bool> UpdateStatusBookingToCompleted(Guid? bookingId);

		Task<List<Booking>> GetBookingOverTimeAsync();
	}
}
