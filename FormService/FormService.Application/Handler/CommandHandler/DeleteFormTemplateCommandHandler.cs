using FormService.Application.Command;
using FormService.Application.DTOs.Response;
using FormService.Application.Event;
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
    public class DeleteFormTemplateCommandHandler : IRequestHandler<DeleteFormTemplateCommand, bool>
    {
        private readonly IFormTemplateRepositoryWrite _formTemplateRepositoryWrite;
        private readonly IPublishEndpoint _publishEndpoint;
        public DeleteFormTemplateCommandHandler(IFormTemplateRepositoryWrite formTemplateRepositoryWrite, IPublishEndpoint publishEndpoint)
        {
            _formTemplateRepositoryWrite = formTemplateRepositoryWrite;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<bool> Handle(DeleteFormTemplateCommand request, CancellationToken cancellationToken)
        {
           var result = await _formTemplateRepositoryWrite.DeleteFormTemplateAsync(request.id);
			if (!result)
			{
				return false;
			}
			await _publishEndpoint.Publish(new FormTemplateDeletedEvent
            {
                Id = request.id
            }, cancellationToken);
            return true;
        }
    }
}
