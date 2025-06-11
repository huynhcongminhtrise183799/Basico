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
	public class AccountRepositoryWrite : IAccountRepositoryWrite
	{
		private readonly AccountDbContextWrite _context;

		public AccountRepositoryWrite(AccountDbContextWrite context)
		{
			_context = context;
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
 

        public async Task AddAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }

        public Task<bool> ExistsByEmailAsync(string email)
        {
            return _context.Accounts.AnyAsync(a => a.AccountEmail == email);
        }

        public Task<bool> ExistsByUsernameAsync(string username)
        {
            return _context.Accounts.AnyAsync(a => a.AccountUsername == username);
        }

        public async Task<Account?> GetAccountById(Guid accountId)
        {
            return await _context.Accounts.FindAsync(accountId);
		}

        public async Task UpdateAccount(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
		}

		// Staff management methods
		public async Task AddStaff(Account staff)
		{
			staff.AccountRole = Role.STAFF.ToString(); // đảm bảo role là staff
			staff.AccountStatus = Status.ACTIVE.ToString(); // mặc định trạng thái là ACTIVE
			await _context.Accounts.AddAsync(staff);
			await _context.SaveChangesAsync();
		}

		public async Task<bool> UpdateStaff(Account staff)
		{
			var existing = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == staff.AccountId && a.AccountRole == "STAFF");
			if (existing == null) return false;

			existing.AccountFullName = staff.AccountFullName;
			existing.AccountEmail = staff.AccountEmail;
			existing.AccountPhone = staff.AccountPhone;
			existing.AccountGender = staff.AccountGender;

			_context.Accounts.Update(existing);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteStaff(Guid staffId)
		{
			var staff = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == staffId && a.AccountRole == "STAFF");
			if (staff == null) return false;

			staff.AccountStatus = Status.INACTIVE.ToString(); // Chuyển trạng thái sang INACTIVE
			_context.Accounts.Update(staff);
			await _context.SaveChangesAsync();
			return true;
		}


		public async Task<Account?> GetStaffById(Guid staffId)
		{
			return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == staffId && a.AccountRole == "STAFF");
		}
	}
}
