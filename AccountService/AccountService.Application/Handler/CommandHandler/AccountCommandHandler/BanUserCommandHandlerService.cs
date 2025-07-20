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
	public class BanUserCommandHandlerService : IRequestHandler<BanUserCommnad, bool>
	{
		private readonly IAccountRepositoryWrite _repoWrite;
		private readonly IPublishEndpoint _publish;
		public BanUserCommandHandlerService(IAccountRepositoryWrite repoWrite, IPublishEndpoint publish)
		{
			_repoWrite = repoWrite;
			_publish = publish;
		}

		public async Task<bool> Handle(BanUserCommnad request, CancellationToken cancellationToken)
		{
			var result =  await _repoWrite.BanUserAccount(request.accountId);
			if (!result)
			{
				return false; // Ban user failed
			}
			var @event = new BanAccountEvent
			{
				AccountId = request.accountId,
			};
			await _publish.Publish(@event, cancellationToken);
			return true;
		}
	}
}
