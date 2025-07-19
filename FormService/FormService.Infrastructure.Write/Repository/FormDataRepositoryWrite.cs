using FormService.Domain.Entities;
using FormService.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Infrastructure.Write.Repository
{
	public class FormDataRepositoryWrite : IFormDataRepositoryWrite
	{
		private readonly FormDbContextWrite _context;
		public FormDataRepositoryWrite(FormDbContextWrite context)
		{
			_context = context ;
		}
		public async Task<bool> AddAsync(CustomerForm customerForm)
		{
			try
			{
				await _context.CustomerForms.AddAsync(customerForm);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{

				return false;
			}
		}

		public async Task<bool> UpdateAsync(CustomerForm customerForm)
		{
			var existingForm = await _context.CustomerForms.FirstOrDefaultAsync(cf => cf.CustomerFormId == customerForm.CustomerFormId);
			if (existingForm != null)
			{
				existingForm.CustomerFormData = customerForm.CustomerFormData;
				existingForm.Status = customerForm.Status;
				await _context.SaveChangesAsync();
				return true;
			}
			else
			{
				throw new KeyNotFoundException("Customer form not found.");
			}
		}
	}
}
