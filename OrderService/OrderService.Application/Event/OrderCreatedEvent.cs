using System;
using System.Collections.Generic;
using static OrderService.Application.Event.OrderCreatedEvent;

namespace OrderService.Application.Event
{
    public record OrderCreatedEvent(
        Guid OrderId,
        Guid UserId,
        double TotalPrice,
        string Status,
        List<OrderDetailDto> OrderDetails
    )
    {
        public record OrderDetailDto(
            Guid OrderDetailId,
            Guid? TicketPackageId,
            Guid? FormTemplateId,
            int Quantity,
            double Price
        );
    }
}
