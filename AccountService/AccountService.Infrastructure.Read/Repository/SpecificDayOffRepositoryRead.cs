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
	public class SpecificDayOffRepositoryRead : ISpecificDayOffRepositoryRead
	{
		private readonly AccountDbContextRead _context;
		public SpecificDayOffRepositoryRead(AccountDbContextRead context)
		{
			_context = context;
		}
		public async Task AddAsync(List<SpecificLawyerDayOffSchedule> specificLawyerDayOffSchedules)
		{
			await _context.SpecificLawyerDayOffSchedules.AddRangeAsync(specificLawyerDayOffSchedules);
			await _context.SaveChangesAsync();
		}

		public Task DeleteAsync(Guid lawyerDayOffScheduleId)
		{
			var schedule = _context.SpecificLawyerDayOffSchedules
				.Where(s => s.LawyerDayOffScheduleId == lawyerDayOffScheduleId);
			_context.SpecificLawyerDayOffSchedules.RemoveRange(schedule);
			return _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(List<SpecificLawyerDayOffSchedule> specificLawyerDayOffSchedule, Guid lawyerScheduleId)
		{
			var schedule = await _context.SpecificLawyerDayOffSchedules
				.Where(s => s.LawyerDayOffScheduleId == lawyerScheduleId)
				.ToListAsync();
			if (schedule.Any())
			{
				_context.SpecificLawyerDayOffSchedules.RemoveRange(schedule);
			}
			await _context.SpecificLawyerDayOffSchedules.AddRangeAsync(specificLawyerDayOffSchedule);
			await _context.SaveChangesAsync();
		}
	}
}
