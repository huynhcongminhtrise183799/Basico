using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.DTOs.Request
{
    public class LawyerSpecificServiceDTO
    {
        public Guid LawyerId { get; set; } // Unique identifier for the lawyer

        public Guid ServiceId { get; set; } // Unique identifier for the service

        public double PricePerHour { get; set; } // Price per hour for the service

    }
}
