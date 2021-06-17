using AutoMapper;
using Crowdly_BE.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowdly_BE.Models.Events
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Services.Events.Models.Event, Event>();
            CreateMap<Services.Common.Models.DataPage<Services.Events.Models.Event>, DataPage<Event>>();
            CreateMap<EventsFilters, Services.Events.Models.EventsFilters>();

            CreateMap<Services.Events.Models.EventDetails, EventDetails>();

            CreateMap<CreateEventModel, Services.Events.Models.CreateEventModel>()
                .ForMember(dest => dest.Images, opt => opt.Ignore());
            CreateMap<UpdateEventModel, Services.Events.Models.UpdateEventModel>()
                .ForMember(dest => dest.Images, opt => opt.Ignore());
        }
    }
}
