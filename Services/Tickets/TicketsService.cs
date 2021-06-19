using AutoMapper;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services.Tickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Tickets
{
    public class TicketsService : ITicketsService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public TicketsService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ValidateTicketResponse> ValidateTicketAsync(Guid eventId, Guid ticketId)
        {
            var ticket = await _dbContext.Tickets
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.EventId == eventId && t.Id == ticketId);

            if (ticket is null) return null;

            return new ValidateTicketResponse
            {
                TicketId = ticket.Id,
                UserId = ticket.User.Id,
                Email = ticket.User.Email,
                FirstName = ticket.User.FirstName,
                LastName = ticket.User.LastName,
                ProfileImage = ticket.User.Image,
            };
        }
    }
}
