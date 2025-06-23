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
    public class GetAllFormTemplatesActiveQueryHandler : IRequestHandler<GetAllFormTemplatesActiveQuery, List<FormTemplateResponse>>
    {
        private readonly IFormTemplateRepositoryRead _formTemplateRepositoryRead;
        public GetAllFormTemplatesActiveQueryHandler(IFormTemplateRepositoryRead formTemplateRepositoryRead)
        {
            _formTemplateRepositoryRead = formTemplateRepositoryRead;
        }
        public async Task<List<FormTemplateResponse>> Handle(GetAllFormTemplatesActiveQuery request, CancellationToken cancellationToken)
        {
            var templates = await _formTemplateRepositoryRead.GetAllActiveFormTemplatesAsync();
            var formTemplateResponses = templates.Select(template => new FormTemplateResponse
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
