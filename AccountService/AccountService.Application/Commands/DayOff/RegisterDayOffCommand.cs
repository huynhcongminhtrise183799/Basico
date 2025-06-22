using AccountService.Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Commands.DayOff
{
	public record RegisterDayOffCommand(Guid lawyerId, DateOnly dayOff, List<string> shiftId) : IRequest<RegisterDayOffResponse>;
	
	
}
