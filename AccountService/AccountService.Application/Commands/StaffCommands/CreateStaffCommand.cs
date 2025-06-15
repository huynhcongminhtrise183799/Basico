using MediatR;
using System;

namespace AccountService.Application.Commands.StaffCommands
{
    public class CreateStaffCommand : IRequest<Guid>
    {
        public string FullName { get; }
        public string Email { get; }
        public int Gender { get; }
        public string Password { get; } = string.Empty;

        public string Username { get; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

		public CreateStaffCommand(string fullName, string email, int gender, string password, string username, string imageUrl)
        {
            FullName = fullName;
            Email = email;
            Gender = gender;
            Password = password;
            Username = username;
            ImageUrl = imageUrl;
		}
    }
}