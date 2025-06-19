using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using MassTransit;
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

		// Forgot password methods

		public async Task<Account?> FindByEmailAsync(string email)
		{
			return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountEmail == email);
		}

		public async Task<ForgotPassword> SaveOtpAsync(Guid accountId, string otp, DateTime expiredAt)
		{
			var entity = new ForgotPassword
			{
				ForgotPasswordId = Guid.NewGuid(),
				AccountId = accountId,
				OTP = otp,
				ExpirationDate = expiredAt
			};

			_context.ForgotPasswords.Add(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task<bool> VerifyOtpAsync(string email, string otp)
		{
			var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountEmail == email);
			if (account == null) return false;

			var forgetPassword = await _context.ForgotPasswords
				.Where(f => f.AccountId == account.AccountId && f.OTP == otp)
				.OrderByDescending(f => f.ExpirationDate)
				.FirstOrDefaultAsync();

			if (forgetPassword == null || forgetPassword.ExpirationDate < DateTime.UtcNow)
				return false;

			return true;
		}


		public async Task<bool> ResetPasswordAsync(string email, string hashedPassword)
		{
			var account = await _context.Accounts
				.FirstOrDefaultAsync(a => a.AccountEmail == email);

			if (account == null) return false;

			account.AccountPassword = hashedPassword; // mật khẩu đã được hash từ handler
			await _context.SaveChangesAsync();

			return true;
		}

		public async Task<Guid?> GetAccountIdIfOtpValidAsync(string email, string otp)
		{
			var now = DateTime.UtcNow.AddHours(7); // hoặc giữ UTC nếu hệ thống thống nhất

			var result = await _context.Accounts
				.Where(a => a.AccountEmail == email)
				.Join(
					_context.ForgotPasswords,
					account => account.AccountId,
					fp => fp.AccountId,
					(account, fp) => new { account.AccountId, fp.OTP, fp.ExpirationDate }
				)
				.Where(x => x.OTP == otp && x.ExpirationDate > now)
				.OrderByDescending(x => x.ExpirationDate)
				.FirstOrDefaultAsync();

			return result?.AccountId;
		}
        //Lawyer methods
        public async Task AddLawyerAsync(Account account, CancellationToken cancellationToken)
        {
            await _context.Accounts.AddAsync(account, cancellationToken);
        }
     
        public async Task<Account?> GetLawyerByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Accounts.FindAsync(new object[] { id }, cancellationToken);
        }

        public Task UpdateLawyerAsync(Account account, CancellationToken cancellationToken)
        {
            _context.Accounts.Update(account);
            return Task.CompletedTask;
        }

        public Task DeleteLawyerAsync(Account account, CancellationToken cancellationToken)
        {
            _context.Accounts.Remove(account);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        //Service methods
        public async Task<Service> AddServiceAsync(Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            return service;
        }

        public async Task UpdateServiceAsync(Service service)
        {
            _context.Services.Update(service);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteServiceAsync(Guid serviceId)
        {
            var entity = await _context.Services.FirstOrDefaultAsync(x => x.ServiceId == serviceId);
            if (entity != null)
            {
                _context.Services.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Service> GetServiceByIdAsync(Guid serviceId)
        {
            return await _context.Services.FirstOrDefaultAsync(x => x.ServiceId == serviceId);
        }

        public async Task<Account> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(a => a.AccountId == userId, cancellationToken);
        }
    }
}
