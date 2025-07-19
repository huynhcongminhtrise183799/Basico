using AccountService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.IRepositories
{
	public interface ISpecificDayOffRepositoryWrite
	{
		Task<bool> AddAsync(List<SpecificLawyerDayOffSchedule> specificLawyerDayOffSchedules);

		Task<bool> UpdateAsync(List<SpecificLawyerDayOffSchedule> specificLawyerDayOffSchedule, Guid lawyerscheduleId);

		Task<bool> DeleteAsync(Guid lawyerDayOffScheduleId);
	}
}
