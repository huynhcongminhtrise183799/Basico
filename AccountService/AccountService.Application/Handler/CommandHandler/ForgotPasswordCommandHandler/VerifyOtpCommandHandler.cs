using AccountService.Application.Commands.ForgotPasswordCommands;
using AccountService.Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

public class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, bool>
{
	private readonly IAccountRepositoryWrite _repoWrite;
	private readonly IMemoryCache _memoryCache;

	public VerifyOtpCommandHandler(IAccountRepositoryWrite repoWrite, IMemoryCache memoryCache)
	{
		_repoWrite = repoWrite;
		_memoryCache = memoryCache;
	}

	public async Task<bool> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
	{
		return await _repoWrite.VerifyOtpAsync(request.Email, request.OTP);
	}
}
