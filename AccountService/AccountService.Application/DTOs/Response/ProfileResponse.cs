using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.DTOs.Response
{
    public class ProfileResponse
    {
        public Guid AccountId { get; set; }

        public string Username { get; set; }
		public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;

		public int Gender { get; set; }

        public int AccountTicketRequest { get; set; } // 

        public string Role { get; set; } = string.Empty;
	}
}
