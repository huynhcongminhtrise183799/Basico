using System;

namespace AccountService.Application.DTOs.Request
{
    public class StaffUpdateRequest
    {
        public string FullName { get; set; } = string.Empty;
        public int Gender { get; set; }
    }
}