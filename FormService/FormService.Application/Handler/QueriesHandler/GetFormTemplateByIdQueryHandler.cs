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
    public class GetFormTemplateByIdQueryHandler : IRequestHandler<GetFormTemplateByIdQuery, FormTemplateResponse>
    {
        private readonly IFormTemplateRepositoryRead _repo;
        public GetFormTemplateByIdQueryHandler(IFormTemplateRepositoryRead repo)
        {
            _repo = repo;
        }
        public async Task<FormTemplateResponse> Handle(GetFormTemplateByIdQuery request, CancellationToken cancellationToken)
        {
           var formTemplate = await _repo.GetFormTemplateByIdAsync(request.id);
            if (formTemplate == null)
            {
                return null; // or throw an exception if preferred
            }

            return new FormTemplateResponse
            {
                FormTemplateId = formTemplate.FormTemplateId,
                ServiceId = formTemplate.ServiceId,
                FormTemplateName = formTemplate.FormTemplateName,
                FormTemplateData = formTemplate.FormTemplateData,
                Price = formTemplate.Price,
				Status = formTemplate.Status
            };
        }
    }
}
