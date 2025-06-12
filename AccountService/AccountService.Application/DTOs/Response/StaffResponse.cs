using System;

namespace AccountService.Application.DTOs.Response
{
    public class StaffResponse
    {
        public Guid StaffId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Gender { get; set; }
    }
}