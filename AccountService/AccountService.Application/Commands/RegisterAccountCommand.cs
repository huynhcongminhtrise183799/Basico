using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Commands
{
    public class RegisterAccountCommand : IRequest<Guid>
    {
        public string AccountUsername { get; set; }
        public string AccountPassword { get; set; }
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string AccountEmail { get; set; }
        public string AccountFullName { get; set; }
        public int AccountGender { get; set; }

    }
}
