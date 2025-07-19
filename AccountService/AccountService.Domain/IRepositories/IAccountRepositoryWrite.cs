using AccountService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.IRepositories
{
    public interface IAccountRepositoryWrite
    {
        // SaveChanges
        Task SaveChangesAsync();
        Task SaveChangesAsync(CancellationToken cancellationToken);

        // Account
        Task AddAsync(Account account);
        Task<bool> ExistsByEmailAsync(string email);
        Task<bool> ExistsByUsernameAsync(string username);
        Task<Account?> GetAccountById(Guid accountId);
        Task<bool> UpdateAccount(Account account);
        Task<bool> BanUserAccount(Guid accountId);

		Task ActiveUserAccount(Guid accountId);


		// Staff
		Task<bool> AddStaff(Account staff);
        Task<bool> UpdateStaff(Account staff);
        Task<bool> DeleteStaff(Guid staffId);
        Task<Account?> GetStaffById(Guid staffId);

        // Forgot Password
        Task<Account?> FindByEmailAsync(string email);
        Task<ForgotPassword> SaveOtpAsync(Guid accountId, string otp, DateTime expiredAt);
        Task<bool> VerifyOtpAsync(string email, string otp);
        Task<bool> ResetPasswordAsync(string email, string hashedPassword);
        Task<Guid?> GetAccountIdIfOtpValidAsync(string email, string otp);

        // Lawyer
        Task<bool> AddLawyerAsync(Account account, CancellationToken cancellationToken);
        Task<Account?> GetLawyerByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> UpdateLawyerAsync(Account account, CancellationToken cancellationToken);
        Task<bool> DeleteLawyerAsync(Account account, CancellationToken cancellationToken);

        // Service
        Task<bool> AddServiceAsync(Service service);
        Task<bool> UpdateServiceAsync(Service service);
        Task<bool> DeleteServiceAsync(Guid serviceId);
        Task<Service> GetServiceByIdAsync(Guid serviceId);

        // Get by UserId
        Task<Account> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    }
}
