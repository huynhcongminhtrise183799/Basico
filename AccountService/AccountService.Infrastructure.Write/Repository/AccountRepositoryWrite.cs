using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;

namespace AccountService.Infrastructure.Write.Repository
{
	public class AccountRepositoryWrite : IAccountRepositoryWrite
	{
		private readonly AccountDbContextWrite _context;

		public AccountRepositoryWrite(AccountDbContextWrite context)
		{
			_context = context;
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
