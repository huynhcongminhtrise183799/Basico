using BookingService.Domain.Entities;
using BookingService.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Infrastructure.Write.Repository
{
	public class BookingSlotRepositoryWrite : IBookingSlotsRepositoryWrite
	{
		private readonly BookingDbContextWrite _context;
		public BookingSlotRepositoryWrite(BookingDbContextWrite context)
		{
			_context = context;
		}
		public async Task AddBookedSlotAsync(List<BookingSlots> bookingSlots)
		{
			await _context.BookingSlots.AddRangeAsync(bookingSlots);
			await _context.SaveChangesAsync();
		}

		public Task RemoveBookedSlotAsync(Guid slotId, Guid bookingId)
		{
			throw new NotImplementedException();
		}

		public async Task UpdateBookedSlotAsync(List<BookingSlots> bookingSlot)
		{
			var existingSlots = await _context.BookingSlots
				.Where(slot => bookingSlot.Select(b => b.BookingId).Contains(slot.BookingId))
				.ToListAsync();

			 _context.BookingSlots.RemoveRange(existingSlots);
			await _context.BookingSlots.AddRangeAsync(bookingSlot);
			await _context.SaveChangesAsync();

		}
	}
}
