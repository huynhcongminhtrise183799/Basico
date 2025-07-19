using AccountService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.IRepositories
{
    public interface ILawyerDayOffScheduleRepositoryWrite
    {

		Task<bool> AddLawyerDayOffScheduleAsync(LawyerDayOffSchedule lawyerDayOffSchedule);

		Task<bool> UpdateLawyerDayOffScheduleAsync(LawyerDayOffSchedule lawyerDayOffSchedule);
		Task<bool> DeleteLawyerDayOffScheduleAsync(Guid id);
	}
}
