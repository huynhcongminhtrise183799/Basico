using AccountService.Application.Commands;
using AccountService.Application.IService;
using AccountService.Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.CommandHandler
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
           var account = await _accountRepository.GetAccountByUserNameAndPassword(request.username, request.password);
            if (account == null)
            {
                return null;
            }

            // Generate JWT token
            var token = _tokenService.GenerateToken(account);

            return token;
        }
    }
}
