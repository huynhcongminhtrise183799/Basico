using System;

namespace AccountService.Application.DTOs.Request
{
    public class StaffCreateRequest
    {
        public string Username { get; set; } = string.Empty;
		public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Gender { get; set; }
        public string Password { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty; 
	}
}