using FormService.Application.DTOs.Response;
using FormService.Application.Queries;
using FormService.Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Application.Handler.QueriesHandler
{
    public class GetAllFormTemplatesQueryHandlerService : IRequestHandler<GetAllFormTemplatesQuery, List<FormTemplateResponse>>
    {
        private readonly IFormTemplateRepositoryRead _formTemplateRepositoryRead;
        public GetAllFormTemplatesQueryHandlerService(IFormTemplateRepositoryRead formTemplateRepositoryRead)
        {
            _formTemplateRepositoryRead = formTemplateRepositoryRead;
        }
        public async Task<List<FormTemplateResponse>> Handle(GetAllFormTemplatesQuery request, CancellationToken cancellationToken)
        {
            var formTemplates = await _formTemplateRepositoryRead.GetAllFormTemplatesAsync();
            var formTemplateResponses = formTemplates.Select(template => new FormTemplateResponse
            {
                FormTemplateId = template.FormTemplateId,
                ServiceId = template.ServiceId,
                FormTemplateName = template.FormTemplateName,
                FormTemplateData = template.FormTemplateData,
                Price = template.Price,
                Status = template.Status
            }).ToList();
            return formTemplateResponses;
        }
    }
}
