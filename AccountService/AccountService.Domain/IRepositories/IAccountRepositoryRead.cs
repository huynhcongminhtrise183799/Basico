using AccountService.Domain.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.IRepositories
{
    public interface IAccountRepositoryRead
    {
		Task<Account?> GetByEmailAsync(string email);
		Task AddAsync(Account account);
       
        Task<Account?> GetAccountByUserNameAndPassword(string username, string password);
        Task<Account?> GetAccountById(Guid accountId);
        Task<Account?> GetAccountByUserName(string username);

		// Update profile
		Task UpdateAccount(Account account);

		// Staff management
		Task AddStaff(Account staff);
		Task<bool> UpdateStaff(Account staff);
		Task<bool> DeleteStaff(Guid staffId);
		Task<IEnumerable<Account>> GetAllStaff();
		Task<Account?> GetStaffById(Guid staffId);
		Task<IEnumerable<Account>> GetAllActiveStaff();
	}
}
