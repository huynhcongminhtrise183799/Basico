using AccountService.Application.Commands.ForgotPasswordCommands;
using AccountService.Domain.IRepositories;
using MediatR;

public class VerifyOtpCommandHandlerService : IRequestHandler<VerifyOtpCommand, bool>
{
	private readonly IAccountRepositoryWrite _repoWrite;

	public VerifyOtpCommandHandlerService(IAccountRepositoryWrite repoWrite)
	{
		_repoWrite = repoWrite;
	}

	public async Task<bool> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
	{
		return await _repoWrite.VerifyOtpAsync(request.Email, request.OTP);
	}
}
