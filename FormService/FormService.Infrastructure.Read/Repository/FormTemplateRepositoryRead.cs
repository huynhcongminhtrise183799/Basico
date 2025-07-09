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
    public class FormTemplateRepositoryRead : IFormTemplateRepositoryRead
    {
        private readonly FormDbContextRead _context;
        private const int PAGE_SIZE = 1;
        public FormTemplateRepositoryRead(FormDbContextRead context)
        {
            _context = context ;
        }
        public async Task AddFormTemplateAsync(FormTemplate formTemplate)
        {
            await _context.FormTemplates.AddAsync(formTemplate);
            await _context.SaveChangesAsync();
        }

        public Task DeleteFormTemplateAsync(Guid formTemplateId)
        {
            var formTemplate = _context.FormTemplates.Find(formTemplateId);
            if (formTemplate != null)
            {
                formTemplate.Status = "INACTIVE";
                return _context.SaveChangesAsync();
            }
            throw new KeyNotFoundException("Form template not found.");
        }

        public async Task<List<FormTemplate>> GetAllActiveFormTemplatesAsync()
        {
            return await _context.FormTemplates
                .Where(ft => ft.Status == "ACTIVE")
                .ToListAsync();
        }

        public async Task<List<FormTemplate>> GetAllFormTemplatePagition(int page)
        {
            return await _context.FormTemplates
                .Skip((page - 1) * PAGE_SIZE)
                .Take(PAGE_SIZE)
                .ToListAsync();
        }

        public async Task<List<FormTemplate>> GetAllFormTemplatesAsync()
        {
            return await _context.FormTemplates
                .ToListAsync();
        }

        public async Task<FormTemplate?> GetFormTemplateByIdAsync(Guid formTemplateId)
        {
            return await _context.FormTemplates
                .FirstOrDefaultAsync(ft => ft.FormTemplateId == formTemplateId);
        }

        public Task<List<FormTemplate>> GetFormTemplatesByServiceIdAsync(Guid serviceId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateFormTemplateAsync(FormTemplate formTemplate)
        {
            var existingTemplate = _context.FormTemplates.Find(formTemplate.FormTemplateId);
            if (existingTemplate != null)
            {
                existingTemplate.ServiceId = formTemplate.ServiceId;
                existingTemplate.FormTemplateName = formTemplate.FormTemplateName;
                existingTemplate.FormTemplateData = formTemplate.FormTemplateData;
                existingTemplate.Price = formTemplate.Price;
                existingTemplate.Status = formTemplate.Status;
                return _context.SaveChangesAsync();
            }
            throw new KeyNotFoundException("Form template not found.");
        }
    }
}
