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
    }
}
