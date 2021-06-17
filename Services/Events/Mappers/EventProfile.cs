using AutoMapper;
using Services.Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Events.Mappers
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<DataAccess.Models.Event, Event>()
                .ForMember(dest => dest.Thumbnail, opt => opt.MapFrom(source => source.Images.FirstOrDefault()));
            CreateMap<EventCategoryType, DataAccess.Models.EventCategoryType>();

            CreateMap<DataAccess.Models.Event, EventDetails>();

            CreateMap<CreateEventModel, DataAccess.Models.Event>();
            CreateMap<UpdateEventModel, DataAccess.Models.Event>();
        }
    }
}
