using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using MassTransit;
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

        //Lawyer methods
        public async Task AddLawyerAsync(Account account, CancellationToken cancellationToken)
        {
            await _context.Accounts.AddAsync(account, cancellationToken);
        }
     
        public async Task<Account?> GetLawyerByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Accounts.FindAsync(new object[] { id }, cancellationToken);
        }

        public Task UpdateLawyerAsync(Account account, CancellationToken cancellationToken)
        {
            _context.Accounts.Update(account);
            return Task.CompletedTask;
        }

        public Task DeleteLawyerAsync(Account account, CancellationToken cancellationToken)
        {
            _context.Accounts.Remove(account);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        //Service methods
        public async Task<Service> AddServiceAsync(Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            return service;
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

    }
}
