using BookingService.Domain.Entities;
using BookingService.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Infrastructure.Read.Repository
{
	public class BookingRepositoryRead : IBookingRepositoryRead
	{
		private readonly BookingDbContextRead _context;
		public BookingRepositoryRead(BookingDbContextRead context)
		{
			_context = context;
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

		public Task<List<Booking>> GetAllBookingsAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<Booking> GetBookingByIdAsync(Guid bookingId)
		{
			var booking = await _context.Bookings.FindAsync(bookingId);
			if (booking != null)
			{
				return booking;
			}
			else
			{
				throw new KeyNotFoundException("Booking not found");
			}
		}

		public Task<List<Booking>> GetBookingsByCustomerIdAsync(Guid customerId)
		{
			throw new NotImplementedException();
		}

		public Task<List<Booking>> GetBookingsByLawyerIdAsync(Guid lawyerId)
		{
			throw new NotImplementedException();
		}

		public Task<List<Booking>> GetBookingsByServiceIdAsync(Guid serviceId)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Booking>> GetBookingsByCusomterIdAndStatus(Guid id, string status)
		{
			return await _context.Bookings
				.Where(b => b.CustomerId == id && b.Status.ToUpper() == status.ToUpper())
				.ToListAsync();
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

		public async Task<List<Booking>> GetBookingsByLawyerIdAndStatus(Guid id, string status, DateOnly bookingDate)
		{
			return await _context.Bookings
				.Where(b => b.LawyerId == id && b.Status.ToUpper() == status.ToUpper() && b.BookingDate == bookingDate)
				.ToListAsync();
		}

		public async Task<List<Booking>> GetBookingsByStatusInDay(DateOnly bookingDate, string status)
		{
			return await _context.Bookings.Where(b => b.BookingDate == bookingDate && b.Status.ToUpper() == status.ToUpper()).ToListAsync();
		}

		public async Task<bool> CheckInBooking(Guid bookingId)
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
	}

}
