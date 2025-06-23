using AccountService.Application.Commands.AccountCommands;
using AccountService.Application.IService;
using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.CommandHandler.AccountHandler
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly ITokenService _tokenService;
        private readonly IAccountRepositoryRead _accountRepository;

        public LoginUserCommandHandler(ITokenService tokenService, IAccountRepositoryRead accountRepository)
        {
            _tokenService = tokenService;
            _accountRepository = accountRepository;
        }
        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
           var account = await _accountRepository.GetAccountByUserName(request.username);

            if (account == null)
            {
                return null;
            }
            if(account.AccountStatus.ToUpper() == Status.INACTIVE.ToString())
            {
                return "Account are banned"; // Account is inactive
			}
            PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(null, account.AccountPassword, request.password);
            if (passwordVerificationResult != PasswordVerificationResult.Success)
            {
                return null; // Invalid credentials
            }
            

            // Generate JWT token
            var token = _tokenService.GenerateToken(account);

            return token;
        }
    }
}
