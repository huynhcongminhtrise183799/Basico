using MediatR;

namespace AccountService.Application.Commands.AccountCommands
{

    public record LoginUserCommand(string username, string password) : IRequest<string>;
}
