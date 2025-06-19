using BookingService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Domain.IRepository
{
	public interface IBookingSlotsRepositoryWrite
	{
		Task AddBookedSlotAsync(List<BookingSlots> bookingSlots);

		Task UpdateBookedSlotAsync(List<BookingSlots> bookingSlot);

		Task RemoveBookedSlotAsync(Guid slotId, Guid bookingId);
	}
}
