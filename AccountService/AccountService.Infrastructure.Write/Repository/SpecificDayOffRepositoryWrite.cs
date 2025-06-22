using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Infrastructure.Write.Repository
{
	public class SpecificDayOffRepositoryWrite : ISpecificDayOffRepositoryWrite
	{
		private readonly AccountDbContextWrite _context;
		public SpecificDayOffRepositoryWrite(AccountDbContextWrite context)
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
			var schedules = _context.SpecificLawyerDayOffSchedules.Where(s => s.LawyerDayOffScheduleId == lawyerDayOffScheduleId);
			_context.SpecificLawyerDayOffSchedules.RemoveRange(schedules);
			return _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(List<SpecificLawyerDayOffSchedule> specificLawyerDayOffSchedule, Guid lawyerScheduleId)
		{
			// Xóa các ngày nghỉ hiện tại của luật sư
			var schedule = await _context.SpecificLawyerDayOffSchedules.Where(s => s.LawyerDayOffScheduleId == lawyerScheduleId).ToListAsync();
			if (schedule.Any())
			{
				 _context.SpecificLawyerDayOffSchedules.RemoveRange(schedule);
			}
			// Thêm các ngày nghỉ mới vào context
			_context.SpecificLawyerDayOffSchedules.AddRange(specificLawyerDayOffSchedule);

			await _context.SaveChangesAsync();
		}
	}

}
