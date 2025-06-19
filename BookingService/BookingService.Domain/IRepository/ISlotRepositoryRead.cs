using BookingService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Domain.IRepository
{
	public interface ISlotRepositoryRead
	{
		Task<List<Slot>> GetBusySlots(Guid LawyerId , DateOnly bookingDate);

		Task<List<Slot>> GetBusySlotsForUpdate(Guid bookingId, Guid LawyerId, DateOnly bookingDate);

		Task<List<Slot>> GetSlotsByBookingId(Guid bookingId);	

		Task<List<Slot>> GetAllSlots();

	}
}
