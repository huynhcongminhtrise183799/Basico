using AccountService.Domain.Entity;
using MediatR;
using System;

namespace AccountService.Application.Commands.AccountCommands
{
    public class UpdateProfileCommand : IRequest<bool>
    {
        public Guid AccountId { get; }
        public string FullName { get; }
        public int Gender { get; }

        public string Image { get; set; } = string.Empty;

		public UpdateProfileCommand(Guid accountId, string fullName, int gender, string image)
        {
            AccountId = accountId;
            FullName = fullName;
            Gender = gender;
			Image = image;
		}
    }
}