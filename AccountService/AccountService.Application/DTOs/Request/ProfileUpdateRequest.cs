using AccountService.Domain.Entity;
using System;

namespace AccountService.Application.DTOs.Request
{
    public class ProfileUpdateRequest
    {
        public string FullName { get; set; } = string.Empty;
        public int Gender { get; set; }

        public string Image { get; set; } = string.Empty;
	}
}