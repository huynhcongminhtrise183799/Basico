using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Event.ForgotPasswordEvent
{
	public record ForgotPasswordEvent(Guid AccountId, string Otp, DateTimeOffset ExpirationDate, Guid ForgetPasswordId);

}