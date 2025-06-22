using AccountService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.IRepositories
{
	public interface ILawyerDayOffScheduleRepositoryRead
	{
		Task AddLawyerDayOffScheduleAsync(LawyerDayOffSchedule lawyerDayOffSchedule);
		Task UpdateLawyerDayOffScheduleAsync(LawyerDayOffSchedule lawyerDayOffSchedule);
		Task DeleteLawyerDayOffScheduleAsync(Guid id);

		Task<List<LawyerDayOffSchedule>> GetDayOffBetweenDay(DateOnly fromDate, DateOnly toDate);
		Task<LawyerDayOffSchedule> GetDayOffById(Guid lawyerDayOffScheduleId);
	}
}
