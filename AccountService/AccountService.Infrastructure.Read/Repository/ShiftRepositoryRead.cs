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
	public class ShiftRepositoryRead : IShiftRepositoryRead
	{
		private readonly AccountDbContextRead _accountDbContextRead;
		public ShiftRepositoryRead(AccountDbContextRead accountRead)
		{
			_accountDbContextRead = accountRead;
		}

		public async Task<List<Shift>> GetAll()
		{
			return await _accountDbContextRead.Shifts.ToListAsync();
		}

		public async Task<List<Shift>> GetShiftsOffByLawyerInDay(Guid lawyerId, DateOnly offDay)
		{
			return await _accountDbContextRead.SpecificLawyerDayOffSchedules
				.Where(s => s.LawyerDayOffSchedule.LawyerId == lawyerId && s.LawyerDayOffSchedule.OffDay == offDay && s.Status == ShiftStatus.APPROVED.ToString())
				.Select(s => s.Shift)
				.ToListAsync();
		}
	}

}
