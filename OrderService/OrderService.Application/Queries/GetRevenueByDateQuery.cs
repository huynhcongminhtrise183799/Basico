using MediatR;
using OrderService.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Queries
{
	public record GetRevenueByDateQuery(string StartDate, string EndDate) : IRequest<List<RevenueResponse>>;

}
