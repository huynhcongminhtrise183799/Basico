using MediatR;
using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using Microsoft.AspNetCore.Identity;
using MassTransit;
using AccountService.Application.Event;
using AccountService.Application.Commands.AccountCommands;
using AccountService.Application.Event.AccountEvent;

namespace AccountService.Application.Handler.CommandHandler.AccountHandler
{
    public class RegisterAccountCommandHandlerService : IRequestHandler<RegisterAccountCommand, Guid>
    {
        private readonly IAccountRepositoryWrite _accountRepositoryWrite;
        private readonly IPublishEndpoint _publishEndpoint;


        public RegisterAccountCommandHandlerService(IAccountRepositoryWrite accountRepositoryWrite, IPublishEndpoint publishEndpoint)
        {
            _accountRepositoryWrite = accountRepositoryWrite;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Guid> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
        {
            if (request.AccountPassword != request.ConfirmPassword)
                throw new Exception("Password and Confirm Password do not match.");
            if (await _accountRepositoryWrite.ExistsByUsernameAsync(request.AccountUsername))
                throw new Exception("Username already exists.");
            if (await _accountRepositoryWrite.ExistsByEmailAsync(request.AccountEmail))
                throw new Exception("Email already exists.");

            PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();

            var account = new Account
            {
                AccountId = Guid.NewGuid(),
                AccountUsername = request.AccountUsername,
                AccountPassword = _passwordHasher.HashPassword(null,request.AccountPassword),
                AccountEmail = request.AccountEmail,
                AccountFullName = request.AccountFullName,
                AccountGender = request.AccountGender,
                AccountRole = Role.USER.ToString(),
                AccountStatus = Status.ACTIVE.ToString(),
                AccountTicketRequest = 0
            };
            await _accountRepositoryWrite.AddAsync(account);

            var accountRegisteredEvent = new AccountRegisteredEvent
            {
                AccountId = account.AccountId,
                AccountUsername = account.AccountUsername,
                AccountEmail = account.AccountEmail,
                AccountFullName = account.AccountFullName,
                AccountGender = account.AccountGender,
                AccountRole = account.AccountRole,
                AccountStatus = account.AccountStatus,
                AccountTicketRequest = account.AccountTicketRequest,
                AccountPassword = account.AccountPassword
            };

            await _publishEndpoint.Publish(accountRegisteredEvent, cancellationToken);

            return account.AccountId;
        }

    }
}
