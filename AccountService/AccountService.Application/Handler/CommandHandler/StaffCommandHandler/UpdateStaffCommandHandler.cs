using AccountService.Application.Commands.StaffCommands;
using AccountService.Application.Event;
using AccountService.Application.Event.StaffEvent;
using AccountService.Domain.Entity;
using AccountService.Domain.IRepositories;
using MassTransit;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.CommandHandler.StaffHandler
{
    public class UpdateStaffCommandHandler : IRequestHandler<UpdateStaffCommand, bool>
    {
        private readonly IAccountRepositoryWrite _repoWrite;
        private readonly IPublishEndpoint _publishEndpoint;

		public UpdateStaffCommandHandler(IAccountRepositoryWrite repoWrite, IPublishEndpoint publishEndpoint)
        {
            _repoWrite = repoWrite;
            _publishEndpoint = publishEndpoint;
		}

        public async Task<bool> Handle(UpdateStaffCommand request, CancellationToken cancellationToken)
        {
            var staff = await _repoWrite.GetStaffById(request.StaffId);
            if (staff == null) return false;

            staff.AccountFullName = request.FullName;
            staff.AccountGender = request.Gender;
			staff.AccountStatus = Status.ACTIVE.ToString();
            staff.AccountImage = request.ImageUrl;
			await _repoWrite.UpdateStaff(staff);
            await _publishEndpoint.Publish(new StaffUpdateEvent(staff.AccountId, staff.AccountFullName, staff.AccountGender, Status.ACTIVE, staff.AccountImage));
			return true;
        }
    }
}