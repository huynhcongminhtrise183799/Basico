using AccountService.Application.Commands.StaffCommands;
using AccountService.Application.Event;
using AccountService.Application.Event.StaffEvent;
using AccountService.Domain.IRepositories;
using MassTransit;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.CommandHandler.StaffHandler
{
    public class DeleteStaffCommandHandler : IRequestHandler<DeleteStaffCommand, bool>
    {
        private readonly IAccountRepositoryWrite _repoWrite;
        private readonly IPublishEndpoint _publishEndpoint;

		public DeleteStaffCommandHandler(IAccountRepositoryWrite repoWrite, IPublishEndpoint publishEndpoint)
        {
            _repoWrite = repoWrite;
            _publishEndpoint = publishEndpoint;
		}

        public async Task<bool> Handle(DeleteStaffCommand request, CancellationToken cancellationToken)
        {
			var result = await _repoWrite.DeleteStaff(request.StaffId);
            if (!result) return false;
			await _publishEndpoint.Publish(new StaffDeletedEvent(request.StaffId), cancellationToken);


            return true;
		}
    }
}