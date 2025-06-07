using MediatR;
using AccountService.Application.Commands;
using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Generators;
using DryIoc.ImTools;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace AccountService.Application.Handler.CommandHandler
{
    public class RegisterAccountCommandHandler : IRequestHandler<RegisterAccountCommand, Guid>
    {
        private readonly IAccountRepositoryWrite _accountRepositoryWrite;

        public RegisterAccountCommandHandler(IAccountRepositoryWrite accountRepositoryWrite)
        {
            _accountRepositoryWrite = accountRepositoryWrite;
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

            return account.AccountId;
        }

    }
}
