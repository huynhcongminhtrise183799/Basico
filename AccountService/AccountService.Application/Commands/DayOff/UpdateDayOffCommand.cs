using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Commands.DayOff
{
	public record UpdateDayOffCommand(Guid lawyerDayOffScheduleId,DateOnly dayOff, List<string> shiftId) : IRequest<bool>;
	
	
}
