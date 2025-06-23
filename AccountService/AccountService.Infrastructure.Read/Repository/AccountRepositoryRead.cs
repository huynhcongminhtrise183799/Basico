using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AccountService.Infrastructure.Read.Repository
{
	public class AccountRepositoryRead : IAccountRepositoryRead
	{
		private readonly AccountDbContextRead _context;

		public AccountRepositoryRead(AccountDbContextRead context)
		{
			_context = context;
		}

		public async Task<Account?> GetByEmailAsync(string email)
		{
			return await _context.Accounts.FirstOrDefaultAsync(x => x.AccountEmail == email);
		}

		public async Task AddAsync(Account account)
		{
			_context.Accounts.Add(account);
			await _context.SaveChangesAsync();
		}
	

        public async Task<Account?> GetAccountByUserNameAndPassword(string username, string password)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(a => a.AccountUsername == username && a.AccountPassword == password);
        }

        public async Task<Account?> GetAccountById(Guid accountId)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(a => a.AccountId == accountId);
        }

        public async Task<Account?> GetAccountByUserName(string username)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(a => a.AccountUsername == username);
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
		public async Task<IEnumerable<Account>> GetAllStaff()
		{
			return await _context.Accounts
				.Where(a => a.AccountRole == "STAFF")
				.ToListAsync();
		}

		public async Task<Account?> GetStaffById(Guid staffId)
		{
			return await _context.Accounts
				.FirstOrDefaultAsync(a => a.AccountId == staffId && a.AccountRole == "STAFF");
		}

		public async Task<IEnumerable<Account>> GetAllActiveStaff()
		{
			return await _context.Accounts
				.Where(a => a.AccountRole == "STAFF" && a.AccountStatus == "ACTIVE")
				.ToListAsync();
		}

		// Forgot password
		public async Task SaveOtpAsync(Guid accountId, string otp, DateTime expiredAt, Guid forgetPasswordId)
		{
			var entity = new ForgotPassword
			{
				ForgotPasswordId = forgetPasswordId,
				AccountId = accountId,
				OTP = otp,
				ExpirationDate = expiredAt
			};

			_context.ForgotPasswords.Add(entity);
			await _context.SaveChangesAsync();
		}

        //Lawyer
        public async Task<Account?> GetLawyerByIdAsync(Guid id, CancellationToken cancellationToken)
        {
			return await _context.Accounts
			   .Include(a => a.LawyerSpecificServices)
				.FirstAsync(a => a.AccountId == id && a.AccountRole == "LAWYER", cancellationToken);
		}

        public async Task<IEnumerable<Account>> GetAllLawyersAsync(CancellationToken cancellationToken)
        {
            return _context.Accounts
				.Include(a => a.LawyerSpecificServices)
				.Where(a => a.AccountRole == "LAWYER")
                .ToList();
        }
        public async Task<List<Account>> GetAllActiveLawyerAccountsAsync(CancellationToken cancellationToken)
        {
            return await _context.Accounts
                .Where(a => a.AccountStatus == "ACTIVE" && a.AccountRole == "LAWYER")
                .ToListAsync(cancellationToken);
        }

        public async Task AddLawyerAsync(Account account, CancellationToken cancellationToken = default)
        {
            await _context.Accounts.AddAsync(account, cancellationToken);
        }

        public async Task UpdateLawyerAsync(Account account, CancellationToken cancellationToken = default)
        {
            _context.Accounts.Update(account);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        //Service
        public async Task AddServiceAsync(Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
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
				entity.Status = ServiceStatus.Inactive.ToString();
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Service> GetServiceByIdAsync(Guid serviceId)
        {
            return await _context.Services.FirstOrDefaultAsync(x => x.ServiceId == serviceId);
        }

        public async Task<IEnumerable<Service>> GetAllServiceAsync()
        {
            return await _context.Services.ToListAsync();
        }

		public async Task<List<Account>> GetLaywersByServiceId(Guid serviceId)
		{
			return await _context.Accounts
				.Include(a => a.LawyerSpecificServices)
				.Where(a => a.AccountRole == "LAWYER" && a.LawyerSpecificServices.Any(s => s.ServiceId == serviceId))
				.ToListAsync();
		}

		public async Task<string?> GetLaywerNameByLawyerId(Guid id)
		{
			return await _context.Accounts
				.Where(a => a.AccountId == id && a.AccountRole == "LAWYER")
				.Select(a => a.AccountFullName)
				.FirstOrDefaultAsync();
		}

		public async Task<string?> GetCustomerNameByCustomerId(Guid? customerId)
		{
			return await _context.Accounts
				.Where(a => a.AccountId == customerId)
				.Select(a => a.AccountFullName)
				.FirstOrDefaultAsync();
		}
	
        public async Task UpdateAccountTicketRequestAsync(Guid userId, Guid ticketPackageId, int accountTicketRequest)
        {
            var account = await _context.Accounts
      .FirstOrDefaultAsync(x => x.AccountId == userId);

            if (account == null)
            {
                throw new Exception("Account not found");
            }

            account.AccountTicketRequest += accountTicketRequest;

            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }
        public async Task<Account?> GetByUserIdAsync(Guid userId)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(a => a.AccountId == userId);
        }

		public async Task<List<Account>> GetAllUserAccounts()
		{
			return await _context.Accounts
				.Where(a => a.AccountRole.ToUpper() == Role.USER.ToString().ToUpper())
				.ToListAsync();
		}

		public async Task BanUserAccount(Guid accountId)
		{
			var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId);
			if (account == null)
			{
				throw new Exception("Account not found");
			}
			account.AccountStatus = Status.INACTIVE.ToString(); // Chuyển trạng thái sang INACTIVE
			await _context.SaveChangesAsync();
		}

		public async Task ActiveUserAccount(Guid accountId)
		{
			var account = _context.Accounts.FirstOrDefault(a => a.AccountId == accountId);
			if (account == null)
			{
				throw new Exception("Account not found");
			}
			account.AccountStatus = Status.ACTIVE.ToString(); // Chuyển trạng thái sang ACTIVE
			await _context.SaveChangesAsync();
		}

		public async Task<Account?> GetByPhoneAsync(string phone)
		{
			return await _context.Accounts.FirstAsync(x => x.AccountPhone == phone);
		}
	}
}
