using BookingService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Domain.IRepository
{
	public interface IFeedbackRepositoryWrite
	{
		Task AddAsync(Feedback feedback);

		Task UpdateAsync(Feedback feedback);


	}
}
