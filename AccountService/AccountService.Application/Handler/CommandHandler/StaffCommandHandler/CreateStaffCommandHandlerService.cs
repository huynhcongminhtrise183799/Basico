using AccountService.Application.Commands.StaffCommands;
using AccountService.Application.Event;
using AccountService.Application.Event.StaffEvent;
using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.CommandHandler.StaffHandler
{
    public class CreateStaffCommandHandlerService : IRequestHandler<CreateStaffCommand, Guid>
    {
        private readonly IAccountRepositoryWrite _repoWrite;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateStaffCommandHandlerService(IAccountRepositoryWrite repoWrite, IPublishEndpoint publishEndpoint)
        {
            _repoWrite = repoWrite;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Guid> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
        {
			PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();
			var staff = new Account
            {
				AccountFullName = request.FullName,
                AccountEmail = request.Email,
                AccountGender = request.Gender,
				AccountPassword = _passwordHasher.HashPassword(null, request.Password),
                AccountUsername = request.Username,
                AccountImage = request.ImageUrl,
			};
           var result =  await _repoWrite.AddStaff(staff);
			if (!result)
            {
                return Guid.Empty;
			}
				await _publishEndpoint.Publish(
	                 new StaffCreatedEvent(staff.AccountId, staff.AccountFullName, staff.AccountEmail, staff.AccountPassword, staff.AccountUsername, staff.AccountGender, staff.AccountImage));
			return staff.AccountId;
        }
    }
}