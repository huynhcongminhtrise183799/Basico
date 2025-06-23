
namespace Contracts.Events
{
    public class OrderTicketPackageCreatedEvent
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public Guid TicketPackageId { get; set; }
        public int Quantity { get; set; }
        public int RequestAmount { get; set; }
        public double Price { get; set; }
    }
}
