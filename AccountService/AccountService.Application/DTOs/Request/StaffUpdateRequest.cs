using System;

namespace AccountService.Application.DTOs.Request
{
    public class StaffUpdateRequest
    {
		public Guid StaffId { get; set; }
		public string FullName { get; set; } = string.Empty;
        public int Gender { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
	}
}