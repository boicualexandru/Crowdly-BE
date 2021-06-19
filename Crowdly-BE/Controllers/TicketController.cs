using AutoMapper;
using Crowdly_BE.Models.Tickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowdly_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ITicketsService _ticketsService;
        private readonly IMapper _mapper;

        public TicketController(IAuthorizationService authorizationService, ITicketsService ticketsService, IMapper mapper)
        {
            _authorizationService = authorizationService;
            _ticketsService = ticketsService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Validate")]
        public async Task<ActionResult<ValidateTicketResponse>> GetEventsAsync([FromBody] ValidateTicketRequest validateTicketRequest)
        {
            var response = await _ticketsService.ValidateTicketAsync(validateTicketRequest.EventId, validateTicketRequest.TicketId);

            return Ok(_mapper.Map<ValidateTicketResponse>(response));
        }

    }
}
