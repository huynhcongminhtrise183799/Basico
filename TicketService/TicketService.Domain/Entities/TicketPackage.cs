using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketService.Domain.Entities
{
    public enum Status
    {
        ACTIVE, INACTIVE
    }
    public class TicketPackage
    {
        public Guid TicketPackageId { get; set; }

        public string TicketPackageName { get; set; }

        public int RequestAmount { get; set; }

        public double Price { get; set; }

        public string Status { get; set; }
    }
}
