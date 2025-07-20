using Contracts.Events;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Application.Command.Ticket;
using TicketService.Domain.Entities;
using TicketService.Domain.IRepositories;
namespace TicketService.Application.Handler.CommandHandler.Ticket
{
	public class ReplyTicketCommandHandlerService : IRequestHandler<ReplyTicketCommand, bool>
	{
		private readonly ITicketRepositoryWrite _repoWrite;
		private readonly IPublishEndpoint _publish;
		public ReplyTicketCommandHandlerService(ITicketRepositoryWrite repoWrite, IPublishEndpoint publishEndpoint)
		{
			_repoWrite = repoWrite;
			_publish = publishEndpoint;
		}
		public async Task<bool> Handle(ReplyTicketCommand request, CancellationToken cancellationToken)
		{
			var ticket = new Domain.Entities.Ticket
			{
				TicketId = request.TicketId,
				StaffId = request.StaffId,
				Content_Response = request.Response,
				Status = TicketStatus.ANSWERED.ToString()
			};
			var result = await _repoWrite.ReplyTicket(ticket);
			if (!result) { return false; }
			var @event = new TicketRepliedEvent
			{
				TicketId = request.TicketId,
				StaffId = request.StaffId,
				Response = request.Response,
				Status = TicketStatus.ANSWERED.ToString()
			};
			await _publish.Publish(@event, cancellationToken);
			return true;
		}
	}
}
