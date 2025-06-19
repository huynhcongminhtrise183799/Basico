using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Events
{
    public class GetRequestAmountEvent
    {
        public Guid TicketPackageId { get; set; }
    }
}
