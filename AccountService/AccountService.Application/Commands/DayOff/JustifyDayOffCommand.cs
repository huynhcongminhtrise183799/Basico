using AccountService.Application.DTOs.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Commands.DayOff
{
	public record JustifyDayOffCommand(Guid lawyerIdScheduleId,List<JustifyDayOffRequest> requests) : IRequest<bool>;
	

	}

	