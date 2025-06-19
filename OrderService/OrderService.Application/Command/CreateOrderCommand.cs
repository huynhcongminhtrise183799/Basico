using MediatR;
using System;
using System.Collections.Generic;

namespace OrderService.Application.Command
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public double TotalPrice { get; set; }
        public List<CreateOrderDetailModel> OrderDetails { get; set; }
    }

    public class CreateOrderDetailModel
    {
        public Guid? FormTemplateId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}