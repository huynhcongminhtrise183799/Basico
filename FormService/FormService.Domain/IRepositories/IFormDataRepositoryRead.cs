using FormService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Domain.IRepositories
{
    public interface IFormDataRepositoryRead
    {
		Task AddAsync(CustomerForm customerForm);

		Task UpdateAsync(CustomerForm customerForm);
		Task<List<CustomerForm>?> GetCustomerFormsByCustomerIdAsync(Guid customerId);

		Task<CustomerForm?> GetCustomerFormByIdAsync(Guid customerFormId);
	}
}
