using AccountService.Application.Commands.AccountCommands;
using AccountService.Application.Event;
using AccountService.Application.Event.AccountEvent;
using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using MassTransit;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.CommandHandler.AccountHandler
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, bool>
    {
        private readonly IAccountRepositoryWrite _repoWrite;
        private readonly IPublishEndpoint _publishEndpoint;

		public UpdateProfileCommandHandler(IAccountRepositoryWrite repoWrite, IPublishEndpoint publishEndpoint)
        {
            _repoWrite = repoWrite;
            _publishEndpoint = publishEndpoint;
		}

        public async Task<bool> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var account = await _repoWrite.GetAccountById(request.AccountId);
            if (account == null)
                return false;

            account.AccountFullName = request.FullName;
            account.AccountGender = request.Gender;
			account.AccountImage = request.Image;

			await _repoWrite.UpdateAccount(account);
            await _publishEndpoint.Publish(new UpdatedProfileEvent(account.AccountId, account.AccountFullName, account.AccountGender, account.AccountImage));
			return true;
        }
    }
}