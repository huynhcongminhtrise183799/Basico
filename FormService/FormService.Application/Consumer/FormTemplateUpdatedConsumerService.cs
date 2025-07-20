using FormService.Application.Event;
using FormService.Domain.Entities;
using FormService.Domain.IRepositories;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Application.Consumer
{
    public class FormTemplateUpdatedConsumerService : IConsumer<FormTemplateUpdatedEvent>
    {
        private readonly IFormTemplateRepositoryRead _repo;

        public FormTemplateUpdatedConsumerService(IFormTemplateRepositoryRead repo)
        {
            _repo = repo;
        }
        public async Task Consume(ConsumeContext<FormTemplateUpdatedEvent> context)
        {
            var mess = context.Message;
            var template = new FormTemplate
            {
                FormTemplateId = mess.FormTemplateId,
                ServiceId = mess.ServiceId,
                FormTemplateName = mess.FormTemplateName,
                FormTemplateData = mess.FormTemplateData,
				Price = mess.Price,
				Status = mess.Status
            };
            await _repo.UpdateFormTemplateAsync(template);
        }
    }
}
