using AccountService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.IRepositories
{
	public interface ISpecificDayOffRepositoryRead
	{
		Task AddAsync(List<SpecificLawyerDayOffSchedule> specificLawyerDayOffSchedules);

		Task UpdateAsync(List<SpecificLawyerDayOffSchedule> specificLawyerDayOffSchedule, Guid lawyerScheduleId);
		Task DeleteAsync(Guid lawyerDayOffScheduleId);


	}
}
