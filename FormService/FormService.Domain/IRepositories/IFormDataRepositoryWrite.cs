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
		Task<bool> AddAsync(CustomerForm customerForm);

		Task<bool> UpdateAsync(CustomerForm customerForm);
	}
}
