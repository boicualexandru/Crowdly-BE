using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowdly_BE.Models.Tickets
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Services.Tickets.Models.ValidateTicketResponse, ValidateTicketResponse>();
        }
    }
}
