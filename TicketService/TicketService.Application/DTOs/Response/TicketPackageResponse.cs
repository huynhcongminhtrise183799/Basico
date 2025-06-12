using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketService.Application.DTOs.Response
{
    public class TicketPackageResponse
    {
        public Guid TicketPackageId { get; set; }

        public string TicketPackageName { get; set; }

        public int RequestAmount { get; set; }

        public double Price { get; set; }

        public string Status { get; set; }
    }
}
