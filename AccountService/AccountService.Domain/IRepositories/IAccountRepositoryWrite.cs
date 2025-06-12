using AccountService.Domain.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.IRepositories
{
    public interface IAccountRepositoryWrite
    {
        //Register
        Task AddAsync(Account account);
        Task<bool> ExistsByUsernameAsync(string username);
        Task<bool> ExistsByEmailAsync(string email);

		// Update profile
		Task<Account?> GetAccountById(Guid accountId);
		Task UpdateAccount(Account account);

		// Staff management
		Task AddStaff(Account staff);
		Task<bool> UpdateStaff(Account staff);
		Task<bool> DeleteStaff(Guid staffId);
		Task<Account?> GetStaffById(Guid staffId);

		// Forgot password
		Task<Account?> FindByEmailAsync(string email);
		Task<ForgotPassword> SaveOtpAsync(Guid accountId, string otp, DateTime expirationDate);
		Task<bool> VerifyOtpAsync(string email, string otp);
		Task<bool> ResetPasswordAsync(string email, string newPassword);
		Task<Guid?> GetAccountIdIfOtpValidAsync(string email, string otp);
	}
}
