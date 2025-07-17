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
	public class ServiceRepositoryRead : IServiceRepositoryRead
	{
		private readonly AccountDbContextRead _accountDbContextRead;
		public ServiceRepositoryRead(AccountDbContextRead accountRead)
		{
            _accountDbContextRead = accountRead;
		}

		public async Task<string?> GetServiceNameByServiceId(Guid id)
		{
			return await _accountDbContextRead.Services
				.Where(s => s.ServiceId == id)
				.Select(s => s.ServiceName)
				.FirstOrDefaultAsync();
		}

		public async Task<List<Service>> GetServicesByStatusAsync(string status)
		{
			return await _accountDbContextRead.Services
				.Where(s => s.Status.ToUpper() == status.ToUpper())
				.ToListAsync();
		}
	}
}
