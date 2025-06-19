using FormService.Application.Command;
using FormService.Application.DTOs.Response;
using FormService.Application.Event;
using FormService.Domain.Entities;
using FormService.Domain.IRepositories;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Application.Handler.CommandHandler
{
    public class CreateFormTemplateCommandHandler : IRequestHandler<CreateFormTemplateCommand, FormTemplateResponse>
    {
        private readonly IFormTemplateRepositoryWrite _formTemplateRepositoryWrite;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateFormTemplateCommandHandler(IFormTemplateRepositoryWrite formTemplateRepositoryWrite, IPublishEndpoint publishEndpoint)
        {
            _formTemplateRepositoryWrite = formTemplateRepositoryWrite;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<FormTemplateResponse> Handle(CreateFormTemplateCommand request, CancellationToken cancellationToken)
        {
            var template = new FormTemplate
            {
                FormTemplateId = Guid.NewGuid(),
                FormTemplateData = request.FormTemplateData,
                FormTemplateName = request.FormTemplateName,
                ServiceId = request.ServiceId,
				Price = request.Price,
				Status = Status.ACTIVE.ToString()
            };
            await _formTemplateRepositoryWrite.AddFormTemplateAsync(template);
            var formTemplateCreatedEvent = new FormTemplateCreatedEvent
            {
                FormTemplateId = template.FormTemplateId,
                ServiceId = template.ServiceId,
                FormTemplateName = template.FormTemplateName,
                FormTemplateData = template.FormTemplateData,
				Price = template.Price,
				Status = template.Status
            };
            await _publishEndpoint.Publish(formTemplateCreatedEvent, cancellationToken);
            return new FormTemplateResponse
            {
                FormTemplateId = template.FormTemplateId,
                ServiceId = template.ServiceId,
                FormTemplateName = template.FormTemplateName,
                FormTemplateData = template.FormTemplateData,
				Price = template.Price,
				Status = template.Status
            };

        }
    }
}
