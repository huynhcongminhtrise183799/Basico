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
        Task AddFormTemplateAsync(FormTemplate formTemplate);
        Task UpdateFormTemplateAsync(FormTemplate formTemplate);
        Task DeleteFormTemplateAsync(Guid formTemplateId);
    }
}
