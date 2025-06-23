using AccountService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.IRepositories
{
	public interface IShiftRepositoryRead
	{
		Task<List<Shift>> GetShiftsOffByLawyerInDay(Guid lawyerId, DateOnly offDay);
		Task<List<Shift>> GetAll();
	}
}
