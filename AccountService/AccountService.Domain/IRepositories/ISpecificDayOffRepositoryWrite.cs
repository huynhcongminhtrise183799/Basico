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
		Task AddAsync(List<SpecificLawyerDayOffSchedule> specificLawyerDayOffSchedules);

		Task UpdateAsync(List<SpecificLawyerDayOffSchedule> specificLawyerDayOffSchedule, Guid lawyerscheduleId);

		Task DeleteAsync(Guid lawyerDayOffScheduleId);
	}
}
