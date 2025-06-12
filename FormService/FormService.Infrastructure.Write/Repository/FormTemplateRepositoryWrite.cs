using FormService.Domain.Entities;
using FormService.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Infrastructure.Write.Repository
{
    public class FormTemplateRepositoryWrite : IFormTemplateRepositoryWrite
    {
        private readonly FormDbContextWrite _context;

        public FormTemplateRepositoryWrite(FormDbContextWrite context)
        {
            _context = context;
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

        public Task UpdateFormTemplateAsync(FormTemplate formTemplate)
        {
           var existingTemplate = _context.FormTemplates.Find(formTemplate.FormTemplateId);
            if (existingTemplate != null)
            {
                existingTemplate.ServiceId = formTemplate.ServiceId;
                existingTemplate.FormTemplateName = formTemplate.FormTemplateName;
                existingTemplate.FormTemplateData = formTemplate.FormTemplateData;
                existingTemplate.Status = formTemplate.Status;
                return _context.SaveChangesAsync();
            }
            throw new KeyNotFoundException("Form template not found.");
        }
    }
}
