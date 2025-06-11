using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Entity
{
    public class LawyerSpecificService
    {
        public Guid LawyerId { get; set; } // Unique identifier for the lawyer

        public Guid ServiceId { get; set; } // Unique identifier for the service

        public double PricePerHour { get; set; } // Price charged by the lawyer per hour for this service

        public Account Account { get; set; }

        public Service Service { get; set; } // Navigation property to the Service entity
    }
}
