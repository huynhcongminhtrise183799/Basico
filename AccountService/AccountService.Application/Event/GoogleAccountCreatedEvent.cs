using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Event
{
	public record GoogleAccountCreatedEvent(Guid AccountId,string AccountUsername, string AccountFullName, string AccountEmail, string AccountPassword, string AccountRole, string AccountStatus );
}
