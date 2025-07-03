using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Command
{
    public record CancelPaymentCommand(Guid OrderId) : IRequest<bool>;

}
