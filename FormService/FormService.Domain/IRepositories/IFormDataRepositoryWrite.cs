using FormService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Domain.IRepositories
{
	public interface IFormDataRepositoryWrite
	{
		Task AddAsync(CustomerForm customerForm);

		Task UpdateAsync(CustomerForm customerForm);
	}
}
