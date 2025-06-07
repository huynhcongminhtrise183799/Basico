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

    }
}
