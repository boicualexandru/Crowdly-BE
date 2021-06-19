using Services.Tickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Tickets
{
    public interface ITicketsService
    {
        Task<ValidateTicketResponse> ValidateTicketAsync(Guid eventId, Guid ticketId);
        Task<string[]> Book(Guid eventId, Guid userId);
        Task<Ticket[]> GetTicketsByUserIdAsync(Guid userId);
        Task<Ticket[]> GetTicketsByEventIdAsync(Guid eventId);
    }
}
