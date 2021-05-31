using AutoMapper;

namespace Crowdly_BE.Models.SchedulePeriods
{
    public class SchedulePeriodProfile : Profile
    {
        public SchedulePeriodProfile()
        {
            CreateMap<Services.SchedulePeriods.Models.SchedulePeriod, SchedulePeriod>();

            CreateMap<CreateSchedulePeriodModel, Services.SchedulePeriods.Models.CreateSchedulePeriodModel>()
                .ForMember(dest => dest.VendorId, opt => opt.Ignore())
                .ForMember(dest => dest.BookedByUserId, opt => opt.Ignore());
        }
    }
}
