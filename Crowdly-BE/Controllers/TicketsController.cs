using AutoMapper;
using Crowdly_BE.Authorization;
using Crowdly_BE.Models.Tickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Events;
using Services.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Crowdly_BE.Controllers
{
    [Route("")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ITicketsService _ticketsService;
        private readonly IEventsService _eventsService;
        private readonly IMapper _mapper;

        public TicketsController(IAuthorizationService authorizationService, ITicketsService ticketsService, IEventsService eventsService, IMapper mapper)
        {
            _authorizationService = authorizationService;
            _ticketsService = ticketsService;
            _eventsService = eventsService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Events/{eventId}/ValidateTicket")]
        public async Task<ActionResult<ValidateTicketResponse>> GetEventsAsync([FromRoute] Guid eventId, [FromBody] ValidateTicketRequest validateTicketRequest)
        {
            var existingEvent = await _eventsService.GetByIdAsync(eventId);
            if (existingEvent is null) return NotFound();

            var authorizationResult = await _authorizationService
                .AuthorizeAsync(User, existingEvent, EventOperations.Update);
            if (!authorizationResult.Succeeded) return Unauthorized();

            var response = await _ticketsService.ValidateTicketAsync(eventId, validateTicketRequest.TicketId);

            return Ok(_mapper.Map<ValidateTicketResponse>(response));
        }

        [HttpPost]
        [Route("Events/{eventId}/Book")]
        public async Task<ActionResult> Book([FromRoute] Guid eventId)
        {
            var userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var errorMessages = await _ticketsService.Book(eventId, userId);

            if (errorMessages.Length == 0)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("User/Tickets")]
        public async Task<ActionResult<Ticket[]>> GetTicketsByUser()
        {
            var userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var tickets = await _ticketsService.GetTicketsByUserIdAsync(userId);

            return _mapper.Map<Ticket[]>(tickets);
        }

        [HttpGet]
        [Route("Events/{eventId}/Tickets")]
        public async Task<ActionResult<Ticket[]>> GetTicketsByEvent([FromQuery] Guid eventId)
        {
            var existingEvent = await _eventsService.GetByIdAsync(eventId);
            if (existingEvent is null) return NotFound();

            var authorizationResult = await _authorizationService
                .AuthorizeAsync(User, existingEvent, EventOperations.Update);
            if (!authorizationResult.Succeeded) return Unauthorized();

            var tickets = await _ticketsService.GetTicketsByUserIdAsync(eventId);

            return _mapper.Map<Ticket[]>(tickets);
        }
    }
}
