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
        public async Task<bool> AddFormTemplateAsync(FormTemplate formTemplate)
        {
            try
            {
				await _context.FormTemplates.AddAsync(formTemplate);
				await _context.SaveChangesAsync();
                return true;
			}
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> DeleteFormTemplateAsync(Guid formTemplateId)
        {
          var formTemplate = _context.FormTemplates.Find(formTemplateId);
            if (formTemplate != null)
            {
                formTemplate.Status = "INACTIVE";
                await _context.SaveChangesAsync();
				return true;
			}
            throw new KeyNotFoundException("Form template not found.");
        }

        public async Task<bool> UpdateFormTemplateAsync(FormTemplate formTemplate)
        {
           var existingTemplate = _context.FormTemplates.Find(formTemplate.FormTemplateId);
            if (existingTemplate != null)
            {
                existingTemplate.ServiceId = formTemplate.ServiceId;
                existingTemplate.FormTemplateName = formTemplate.FormTemplateName;
                existingTemplate.FormTemplateData = formTemplate.FormTemplateData;
                existingTemplate.Status = formTemplate.Status;
                await _context.SaveChangesAsync();
                return true;
            }
            throw new KeyNotFoundException("Form template not found.");
        }
    }
}
