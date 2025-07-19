using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Infrastructure.Write.Repository
{
	public class LawyerDayOffScheduleRepositoryWrite : ILawyerDayOffScheduleRepositoryWrite
	{
		private readonly AccountDbContextWrite _context;

		public LawyerDayOffScheduleRepositoryWrite(AccountDbContextWrite context)
		{
			_context = context;
		}

		public async Task<bool> AddLawyerDayOffScheduleAsync(LawyerDayOffSchedule lawyerDayOffSchedule)
		{
			try
			{
				await _context.LawyerDayOffSchedules.AddAsync(lawyerDayOffSchedule);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{

				return false;
			}
		}

		public async Task<bool> DeleteLawyerDayOffScheduleAsync(Guid id)
		{
			var existingSchedule = _context.LawyerDayOffSchedules
				.FirstOrDefault(x => x.LawyerDayOffScheduleId == id);
			if (existingSchedule != null)
			{
				_context.LawyerDayOffSchedules.Remove(existingSchedule);
				await _context.SaveChangesAsync();
				return true;
			}else
			{
				throw new InvalidOperationException("Lawyer Day Off Schedule not found.");
			}
		}

		public async Task<bool> UpdateLawyerDayOffScheduleAsync(LawyerDayOffSchedule lawyerDayOffSchedule)
		{
			var existingSchedule = _context.LawyerDayOffSchedules
				.FirstOrDefault(x => x.LawyerDayOffScheduleId == lawyerDayOffSchedule.LawyerDayOffScheduleId);
			if (existingSchedule != null)
			{
				existingSchedule.OffDay = lawyerDayOffSchedule.OffDay;
				await _context.SaveChangesAsync();
				return true;
			}else
			{
				throw new InvalidOperationException("Lawyer Day Off Schedule not found.");
			}
			

		}
	}
}
