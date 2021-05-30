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
            CreateMap<DataAccess.Models.SchedulePeriod, SchedulePeriod>();

            CreateMap<CreateSchedulePeriodModel, DataAccess.Models.SchedulePeriod>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}
