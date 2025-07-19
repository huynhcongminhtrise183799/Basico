using FormService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Domain.IRepositories
{
    public interface IFormTemplateRepositoryWrite
    {
        Task<bool> AddFormTemplateAsync(FormTemplate formTemplate);
        Task<bool> UpdateFormTemplateAsync(FormTemplate formTemplate);
        Task<bool> DeleteFormTemplateAsync(Guid formTemplateId);
    }
}
