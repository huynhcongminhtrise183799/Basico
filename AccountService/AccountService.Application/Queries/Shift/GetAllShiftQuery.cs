using AccountService.Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Queries.Shift
{
    public record GetAllShiftQuery() : IRequest<List<ShiftResponse>>;


}
