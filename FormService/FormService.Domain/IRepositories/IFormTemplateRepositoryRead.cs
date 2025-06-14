using FormService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Domain.IRepositories
{
    public interface IFormTemplateRepositoryRead
    {
        Task<FormTemplate?> GetFormTemplateByIdAsync(Guid formTemplateId);
        Task<List<FormTemplate>> GetAllFormTemplatesAsync();

        Task<List<FormTemplate>> GetAllActiveFormTemplatesAsync();

        Task<List<FormTemplate>> GetAllFormTemplatePagition(int page);

        Task<List<FormTemplate>> GetFormTemplatesByServiceIdAsync(Guid serviceId);
        Task AddFormTemplateAsync(FormTemplate formTemplate);
        Task UpdateFormTemplateAsync(FormTemplate formTemplate);
        Task DeleteFormTemplateAsync(Guid formTemplateId);
    }
}
