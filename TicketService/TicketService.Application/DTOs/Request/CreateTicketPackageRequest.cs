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
        public int RequestAmount { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }
    }
}
