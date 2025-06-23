using AccountService.Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Queries.DayOff
{
    public record GetDayOffsBetween(DateOnly fromDate, DateOnly toDate) : IRequest<List<DayOffResponse>>;
}
