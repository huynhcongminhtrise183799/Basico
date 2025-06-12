using MediatR;
using System;

namespace AccountService.Application.Commands.StaffCommands
{
    public class DeleteStaffCommand : IRequest<bool>
    {
        public Guid StaffId { get; }

        public DeleteStaffCommand(Guid staffId)
        {
            StaffId = staffId;
        }
    }
}