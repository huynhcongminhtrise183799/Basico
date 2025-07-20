using AccountService.Application.Commands.AccountCommands;
using AccountService.Application.Event.AccountEvent;
using AccountService.Domain.IRepositories;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.CommandHandler.AccountCommandHandler
{
	public class ActiveAccountCommandHandlerService : IRequestHandler<ActiveUserAccountCommand, bool>
	{
		private readonly IAccountRepositoryWrite _repoWrite;
		private readonly IPublishEndpoint _publish;
		public ActiveAccountCommandHandlerService(IAccountRepositoryWrite repoWrite, IPublishEndpoint publish)
		{
			_repoWrite = repoWrite;
			_publish = publish;
		}
		public async Task<bool> Handle(ActiveUserAccountCommand request, CancellationToken cancellationToken)
		{
			await _repoWrite.ActiveUserAccount(request.accountId);
			var @event = new ActiveAccountEvent
			{
				AccountId = request.accountId,
			};
			await _publish.Publish(@event, cancellationToken);
			return true;
		}
	}

}
