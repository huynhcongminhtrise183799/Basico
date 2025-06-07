using AccountService.Domain.Entity;
using System;
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
    }
}
