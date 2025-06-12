using MediatR;

public class ResetPasswordCommand : IRequest<bool>
{
	public string Email { get; set; }
	public string NewPassword { get; }

	public ResetPasswordCommand(string email, string newPassword)
	{
		Email = email;
		NewPassword = newPassword;
	}
}
