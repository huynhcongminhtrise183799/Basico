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
	public class FeedbackRepositoryWrite : IFeedbackRepositoryWrite
	{
		private readonly BookingDbContextWrite _context;

		public FeedbackRepositoryWrite(BookingDbContextWrite context)
		{
			_context = context;
		}

		public async Task<bool> AddAsync(Feedback feedback)
		{
			try
			{
                await _context.Feedbacks.AddAsync(feedback);
                await _context.SaveChangesAsync();
				return true;
            }
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<bool> UpdateAsync(Feedback feedback)
		{
			try
			{
                var existing = await _context.Feedbacks.FirstOrDefaultAsync(f => f.FeedbackId == feedback.FeedbackId);
                if (existing != null)
                {
                    existing.FeedbackContent = feedback.FeedbackContent;
                    existing.Rating = feedback.Rating;
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
					return false;
                }
				
            }
			catch (Exception)
			{

				return false;
			}
		}

	}
}
