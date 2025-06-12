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
        //Lawyer
        public async Task<Account?> GetLawyerByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Accounts.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<IEnumerable<Account>> GetAllLawyersAsync(CancellationToken cancellationToken)
        {
            return _context.Accounts
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
                _context.Services.Remove(entity);
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
    }
}
