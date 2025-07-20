using FormService.Application.Event;
using FormService.Domain.IRepositories;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Application.Consumer
{
    public class FormTemplateDeletedConsumerService : IConsumer<FormTemplateDeletedEvent>
    {
        private readonly IFormTemplateRepositoryRead _repo;
        public FormTemplateDeletedConsumerService(IFormTemplateRepositoryRead repo)
        {
            _repo = repo;
        }
        public async Task Consume(ConsumeContext<FormTemplateDeletedEvent> context)
        {
            await _repo.DeleteFormTemplateAsync(context.Message.Id);
        }
    }
}
