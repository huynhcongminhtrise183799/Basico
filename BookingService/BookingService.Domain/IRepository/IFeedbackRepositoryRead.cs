using BookingService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Domain.IRepository
{
	public interface IFeedbackRepositoryRead
	{
		Task AddAsync(Feedback feedback);

		Task UpdateAsync(Feedback feedback);

		Task<List<Feedback>> GetAllAsync();	

		Task<Feedback> GetByFeedbackIdAsync(Guid feedbackId);

		Task<Feedback?> GetByBookingIdAsync(Guid bookingId);

	}
}
