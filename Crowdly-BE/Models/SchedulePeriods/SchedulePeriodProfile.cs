using AutoMapper;

namespace Crowdly_BE.Models.SchedulePeriods
{
    public class SchedulePeriodProfile : Profile
    {
        public SchedulePeriodProfile()
        {
            CreateMap<Services.SchedulePeriods.Models.VendorSchedulePeriod, VendorSchedulePeriod>();
            CreateMap<Services.SchedulePeriods.Models.UserSchedulePeriod, UserSchedulePeriod>();
            CreateMap<Services.SchedulePeriods.Models.VendorDetails, VendorDetails>();
            CreateMap<Services.SchedulePeriods.Models.UserDetails, UserDetails>();
            CreateMap<Services.SchedulePeriods.Models.Period, Period>();

            CreateMap<CreateSchedulePeriodModel, Services.SchedulePeriods.Models.CreateSchedulePeriodModel>()
                .ForMember(dest => dest.VendorId, opt => opt.Ignore())
                .ForMember(dest => dest.BookedByUserId, opt => opt.Ignore());
        }
    }
}
