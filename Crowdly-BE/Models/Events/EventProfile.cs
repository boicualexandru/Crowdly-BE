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
            CreateMap<Services.Vendors.Models.Vendor, Event>();
            CreateMap<Services.Common.Models.DataPage<Services.Vendors.Models.Vendor>, DataPage<Event>>();
            CreateMap<EventsFilters, Services.Vendors.Models.VendorsFilters>();

            CreateMap<Services.Vendors.Models.VendorDetails, EventDetails>();

            CreateMap<CreateEventModel, Services.Vendors.Models.CreateVendorModel>()
                .ForMember(dest => dest.Images, opt => opt.Ignore());
            CreateMap<UpdateEventModel, Services.Vendors.Models.UpdateVendorModel>()
                .ForMember(dest => dest.Images, opt => opt.Ignore());
        }
    }
}
