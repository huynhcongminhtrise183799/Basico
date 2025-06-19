using BookingService.Domain.Entities;
using BookingService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Infrastructure.Write.Repository
{
	public class BookingRepositoryWrite : IBookingRepositoryWrite
	{
		private readonly BookingDbContextWrite _context;
		public BookingRepositoryWrite(BookingDbContextWrite context)
		{
			_context = context;
		}

		public async Task<bool> CheckInBookingAsync(Guid bookingId)
		{
			var booking = await _context.Bookings.FindAsync(bookingId);
			if (booking == null)
			{
				throw new KeyNotFoundException("Booking not found");
			}
			booking.Status = BookingStatus.CheckedIn.ToString();
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task CreateBookingAsync(Booking booking)
		{
			await _context.Bookings.AddAsync(booking);
			await _context.SaveChangesAsync();
		}

		public Task DeleteBookingAsync(Guid bookingId)
		{
			var booking = _context.Bookings.Find(bookingId);
			if (booking != null)
			{
				booking.Status = BookingStatus.Cancelled.ToString();
				return _context.SaveChangesAsync();
			}
			else
			{
				throw new KeyNotFoundException("Booking not found");
			}
		}

		public async Task UpdateBookingAsync(Booking booking)
		{
			var existingBooking = await _context.Bookings.FindAsync(booking.BookingId);
			if (existingBooking != null)
			{
				existingBooking.CustomerId = booking.CustomerId;
				existingBooking.LawyerId = booking.LawyerId;
				existingBooking.ServiceId = booking.ServiceId;
				existingBooking.BookingDate = booking.BookingDate;
				existingBooking.Status = booking.Status;
				existingBooking.Price = booking.Price;
				// Update other properties as needed

				await _context.SaveChangesAsync();
			}
			else
			{
				throw new KeyNotFoundException("Booking not found");
			}
		}

		public async Task UpdateStatusBookingToPaid(Guid? bookingId)
		{
			var booking = await _context.Bookings.FindAsync(bookingId);
			if (booking != null)
			{
				booking.Status = BookingStatus.Paid.ToString();
				await _context.SaveChangesAsync();
			}
			else
			{
				throw new KeyNotFoundException("Booking not found");
			}
		}
	}
}
