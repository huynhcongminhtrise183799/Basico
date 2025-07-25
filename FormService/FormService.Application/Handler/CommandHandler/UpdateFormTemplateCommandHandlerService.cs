﻿using FormService.Application.Command;
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
    public class UpdateFormTemplateCommandHandlerService : IRequestHandler<UpdateFormTemplateCommand, FormTemplateResponse>
    {
        private readonly IFormTemplateRepositoryWrite _formTemplateRepositoryWrite;
        private readonly IPublishEndpoint _publishEndpoint;

        public UpdateFormTemplateCommandHandlerService(IFormTemplateRepositoryWrite formTemplateRepositoryWrite, IPublishEndpoint publishEndpoint)
        {
            _formTemplateRepositoryWrite = formTemplateRepositoryWrite;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<FormTemplateResponse> Handle(UpdateFormTemplateCommand request, CancellationToken cancellationToken)
        {
            var template = new FormTemplate
            {
                FormTemplateId = request.id,
                ServiceId = request.serviceId,
                FormTemplateName = request.name,
                FormTemplateData = request.data,
				Price = request.Price,
				Status = request.status
            };
            var result = await _formTemplateRepositoryWrite.UpdateFormTemplateAsync(template);
			if (!result)
			{
				return null;
			}
			await _publishEndpoint.Publish(new FormTemplateUpdatedEvent
            {
                FormTemplateId = template.FormTemplateId,
                ServiceId = template.ServiceId,
                FormTemplateName = template.FormTemplateName,
                FormTemplateData = template.FormTemplateData,
				Price = template.Price,
				Status = template.Status
            }, cancellationToken);
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
