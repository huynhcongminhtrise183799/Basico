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
		Task SaveChangesAsync();
	}
}
