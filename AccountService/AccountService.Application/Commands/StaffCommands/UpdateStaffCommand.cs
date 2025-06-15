using MediatR;
using System;

namespace AccountService.Application.Commands.StaffCommands
{
    public class UpdateStaffCommand : IRequest<bool>
    {
        public Guid StaffId { get; }
        public string FullName { get; }
        public int Gender { get; }
        public string ImageUrl { get; set; } = string.Empty;

		public UpdateStaffCommand(Guid staffId, string fullName, int gender, string imageUrl)
        {
            StaffId = staffId;
            FullName = fullName;
            Gender = gender;
            ImageUrl = imageUrl;
		}
    }
}