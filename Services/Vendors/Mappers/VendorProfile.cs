using Services.Vendors.Models;
using AutoMapper;

namespace Services.Vendors.Mappers
{
    class VendorProfile : Profile
    {
        public VendorProfile()
        {
            CreateMap<DataAccess.Models.Vendor, Vendor>();
            CreateMap<VendorCategoryType, DataAccess.Models.VendorCategoryType>();

            CreateMap<DataAccess.Models.Vendor, VendorDetails>();

            CreateMap<CreateVendorModel, DataAccess.Models.Vendor>()
                .ForMember(dest => dest.ThumbnailUrl, opt => opt.Ignore());
            CreateMap<UpdateVendorModel, DataAccess.Models.Vendor>()
                .ForMember(dest => dest.ThumbnailUrl, opt => opt.Ignore());
        }
    }
}
