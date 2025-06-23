using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Infrastructure.Read.Repository
{
	public class LawyerDayOffScheduleRepositoryRead : ILawyerDayOffScheduleRepositoryRead
	{
		private readonly AccountDbContextRead _context;
		public LawyerDayOffScheduleRepositoryRead(AccountDbContextRead context)
		{
			_context = context;
		}
		public async Task AddLawyerDayOffScheduleAsync(LawyerDayOffSchedule lawyerDayOffSchedule)
		{
			await _context.LawyerDayOffSchedules.AddAsync(lawyerDayOffSchedule);
			await _context.SaveChangesAsync();
		}

		public Task DeleteLawyerDayOffScheduleAsync(Guid id)
		{
			var existingSchedule = _context.LawyerDayOffSchedules
				.FirstOrDefault(x => x.LawyerDayOffScheduleId == id);
			if (existingSchedule != null)
			{
				_context.LawyerDayOffSchedules.Remove(existingSchedule);
				return _context.SaveChangesAsync();
			}
			else
			{
				throw new InvalidOperationException("Lawyer Day Off Schedule not found.");
			}
		}

		public async Task<List<LawyerDayOffSchedule>> GetDayOffBetweenDay(DateOnly fromDate, DateOnly toDate)
		{
			return await _context.LawyerDayOffSchedules.Include(x => x.SpecificLawyerDayOffSchedules).ThenInclude(x => x.Shift).Include(x => x.Lawyer)
				.Where(x => x.OffDay >= fromDate && x.OffDay <= toDate)
				.ToListAsync();
		}

		public async Task<LawyerDayOffSchedule> GetDayOffById(Guid lawyerDayOffScheduleId)
		{
			return await _context.LawyerDayOffSchedules.Include(x => x.SpecificLawyerDayOffSchedules).ThenInclude(x => x.Shift).Include(x => x.Lawyer)
					.FirstOrDefaultAsync(x => x.LawyerDayOffScheduleId == lawyerDayOffScheduleId);
		}

		public async Task UpdateLawyerDayOffScheduleAsync(LawyerDayOffSchedule lawyerDayOffSchedule)
		{
			var existingSchedule = _context.LawyerDayOffSchedules
				.FirstOrDefault(x => x.LawyerDayOffScheduleId == lawyerDayOffSchedule.LawyerDayOffScheduleId);
			if (existingSchedule != null)
			{
				existingSchedule.OffDay = lawyerDayOffSchedule.OffDay;
				await _context.SaveChangesAsync();
			}else
			{
				throw new KeyNotFoundException("Lawyer Day Off Schedule not found.");
			}
		}
	}
	
}
