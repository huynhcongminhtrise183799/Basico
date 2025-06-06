using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

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
			await _context.Accounts.AddAsync(account);
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
