using FormService.Domain.Entities;
using FormService.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Infrastructure.Read.Repository
{
	public class FormDataRepositoryRead : IFormDataRepositoryRead
	{
		private readonly FormDbContextRead _context;
		public FormDataRepositoryRead(FormDbContextRead context)
		{
			_context = context;
		}
		public async Task AddAsync(CustomerForm customerForm)
		{
			await _context.CustomerForms.AddAsync(customerForm);
			await _context.SaveChangesAsync();
		}

		public async Task<CustomerForm?> GetCustomerFormByIdAsync(Guid customerFormId)
		{
			return await _context.CustomerForms
				.FirstOrDefaultAsync(cf => cf.CustomerFormId == customerFormId);
		}

		public async Task<List<CustomerForm>?> GetCustomerFormsByCustomerIdAsync(Guid customerId)
		{
			return await _context.CustomerForms
				.Where(cf => cf.CustomerId == customerId)
				.ToListAsync();
		}
		public async Task UpdateAsync(CustomerForm customerForm)
		{
			var existingForm = await _context.CustomerForms.FirstOrDefaultAsync(cf => cf.CustomerFormId == customerForm.CustomerFormId);
			if (existingForm != null)
			{
				existingForm.CustomerFormData = customerForm.CustomerFormData;
				existingForm.Status = customerForm.Status;

				await _context.SaveChangesAsync();
			}
			else
			{
				throw new KeyNotFoundException("Customer form not found.");
			}
		}
	}
}
