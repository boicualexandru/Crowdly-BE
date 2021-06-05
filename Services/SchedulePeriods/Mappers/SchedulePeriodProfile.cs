using AutoMapper;
using Services.SchedulePeriods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SchedulePeriods.Mappers
{
    public class SchedulePeriodProfile : Profile
    {
        public SchedulePeriodProfile()
        {
            CreateMap<DataAccess.Models.SchedulePeriod, VendorSchedulePeriod>();
            CreateMap<DataAccess.Models.SchedulePeriod, UserSchedulePeriod>();
            CreateMap<DataAccess.Models.ApplicationUser, UserDetails>();
            CreateMap<DataAccess.Models.Vendor, VendorDetails>();

            CreateMap<CreateSchedulePeriodModel, DataAccess.Models.SchedulePeriod>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}
