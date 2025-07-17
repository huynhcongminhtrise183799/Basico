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
	public class FeedbackRepositoryRead : IFeedbackRepositoryRead
	{
		private readonly BookingDbContextRead _context;

		public FeedbackRepositoryRead(BookingDbContextRead context)
		{
			_context = context;
		}

		public async Task AddAsync(Feedback feedback)
		{
			await _context.Feedbacks.AddAsync(feedback);
			await _context.SaveChangesAsync();
		}

		public async Task<List<Feedback>> GetAllAsync()
		{
			return await _context.Feedbacks.ToListAsync();
		}

		public async Task<Feedback?> GetByBookingIdAsync(Guid bookingId)
		{
			return await _context.Feedbacks.FirstOrDefaultAsync(f => f.BookingId == bookingId);
		}

		public async Task<Feedback> GetByFeedbackIdAsync(Guid feedbackId)
		{
			var result = await _context.Feedbacks.FirstOrDefaultAsync(f => f.FeedbackId == feedbackId);
			if(result != null)
			{
				return result;
			}
			else
			{
				throw new InvalidOperationException("Feedback not found.");
			}
		}

		public async Task UpdateAsync(Feedback feedback)
		{
			var existing = await _context.Feedbacks.FirstOrDefaultAsync(f => f.FeedbackId == feedback.FeedbackId);
			if (existing != null)
			{
				existing.FeedbackContent = feedback.FeedbackContent;
				existing.Rating = feedback.Rating;
				await _context.SaveChangesAsync();
			}
			else
			{
				throw new InvalidOperationException("Feedback not found.");
			}
		}
	}
}
