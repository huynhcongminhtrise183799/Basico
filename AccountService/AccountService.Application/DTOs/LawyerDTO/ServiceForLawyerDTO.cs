using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.DTOs.LawyerDTO
{
    public class ServiceForLawyerDTO
    {
        public Guid ServiceId { get; set; } // Unique identifier for the service

        public double PricePerHour { get; set; }
    }
}
