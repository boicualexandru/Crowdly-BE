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
    }
}
