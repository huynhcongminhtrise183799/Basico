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
	public class SlotRepositoryRead : ISlotRepositoryRead
	{
		private readonly BookingDbContextRead _context;

		public SlotRepositoryRead(BookingDbContextRead context)
		{
			_context = context;
		}

		public async Task<List<Slot>> GetAllSlots()
		{
			return await _context.Slots.ToListAsync();
		}

		public async Task<List<Slot>> GetBusySlots(Guid LawyerId, DateOnly bookingDate)
		{
			var slots = await (
								from s in _context.Slots
									join bs in _context.BookingSlots on s.SlotId equals bs.SlotId
									join b in _context.Bookings on bs.BookingId equals b.BookingId
									where b.LawyerId == LawyerId && b.BookingDate == bookingDate && b.Status != BookingStatus.Cancelled.ToString()
								select s
									).ToListAsync();
			return slots;

		}

		public async Task<List<Slot>> GetBusySlotsForUpdate(Guid bookingId,Guid LawyerId, DateOnly bookingDate)
		{
			var slots = await(
								from s in _context.Slots
								join bs in _context.BookingSlots on s.SlotId equals bs.SlotId
								join b in _context.Bookings on bs.BookingId equals b.BookingId
								where b.LawyerId == LawyerId && b.BookingDate == bookingDate && b.BookingId != bookingId && b.Status != BookingStatus.Cancelled.ToString()
								select s
									).ToListAsync();
			return slots;
		}

		public async Task<List<Slot>> GetSlotsByBookingId(Guid bookingId)
		{
			var slots = await(
								from s in _context.Slots
								join bs in _context.BookingSlots on s.SlotId equals bs.SlotId
								join b in _context.Bookings on bs.BookingId equals b.BookingId
								where b.BookingId == bookingId
								select s
									).ToListAsync();
			return slots;
		}
	}
}
