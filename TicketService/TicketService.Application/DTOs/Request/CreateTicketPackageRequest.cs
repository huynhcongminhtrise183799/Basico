using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketService.Application.DTOs.Request
{
    public class CreateTicketPackageRequest
    {
        [Required(ErrorMessage ="Name is required")]
        public string TicketPackageName { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Request amount must be greater than 0")]
        public int RequestAmount { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public double Price { get; set; }
    }
}
