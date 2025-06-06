using MediatR;

namespace AccountService.Application.Commands
{

    public record LoginUserCommand(string username, string password) : IRequest<string>;
}
