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

        public async Task<string[]> Book(Guid eventId, Guid userId)
        {
            var dbTicket = new DataAccess.Models.Ticket
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                EventId = eventId,
                UserId = userId,
            };

            await _dbContext.Tickets.AddAsync(dbTicket);
            await _dbContext.SaveChangesAsync();

            return new string[0];
        }

        public async Task<Ticket[]> GetTicketsByEventIdAsync(Guid eventId)
        {
            var dbTickets = await _dbContext.Tickets
                .Where(t => t.EventId == eventId)
                .ToArrayAsync();

            return _mapper.Map<Ticket[]>(dbTickets);
        }

        public async Task<Ticket[]> GetTicketsByUserIdAsync(Guid userId)
        {
            var dbTickets = await _dbContext.Tickets
                .Where(t => t.UserId == userId)
                .ToArrayAsync();

            return _mapper.Map<Ticket[]>(dbTickets);
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
