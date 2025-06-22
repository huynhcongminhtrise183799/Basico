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

		Task AddLawyerDayOffScheduleAsync(LawyerDayOffSchedule lawyerDayOffSchedule);

		Task UpdateLawyerDayOffScheduleAsync(LawyerDayOffSchedule lawyerDayOffSchedule);
		Task DeleteLawyerDayOffScheduleAsync(Guid id);
	}
}
