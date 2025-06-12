using AccountService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Event.StaffEvent
{
	public record StaffUpdateEvent (Guid AccountId, string FullName, int Gender, Status Status);
}
