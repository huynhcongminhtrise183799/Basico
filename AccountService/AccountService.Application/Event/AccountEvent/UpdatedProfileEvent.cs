using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Event.AccountEvent
{
	public record UpdatedProfileEvent (Guid AccountId, string AccountFullName, int AccountGender, string AccountImage);
}
