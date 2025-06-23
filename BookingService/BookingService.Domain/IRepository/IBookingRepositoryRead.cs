using BookingService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Domain.IRepository
{
	public interface IBookingRepositoryRead
	{
		Task<List<Booking>> GetBookingsByCustomerIdAsync(Guid customerId);

		Task<List<Booking>> GetBookingsByLawyerIdAsync(Guid lawyerId);

		Task<List<Booking>> GetBookingsByServiceIdAsync(Guid serviceId);

		Task<Booking> GetBookingByIdAsync(Guid bookingId);

		Task<List<Booking>> GetAllBookingsAsync();
		Task CreateBookingAsync(Booking booking);

		Task UpdateBookingAsync(Booking booking);

		Task DeleteBookingAsync(Guid bookingId);

		Task<List<Booking>> GetBookingsByCusomterIdAndStatus(Guid id, string status);

		Task<List<Booking>> GetBookingsByLawyerIdAndStatus(Guid id, string status, DateOnly bookingDate);

		Task<List<Booking>> GetBookingsByStatusInDay(DateOnly bookingDate, string status);

		Task<bool> CheckInBooking(Guid bookingId);
		Task UpdateStatusBookingToPaid(Guid? bookingId);

		Task UpdateStatusBookingToCompleted(Guid? bookingId);

	}
}
