using AutoMapper;

namespace Crowdly_BE.Vendors
{
    public class VendorProfile : Profile
    {
        public VendorProfile()
        {
            CreateMap<Services.Vendors.Models.Vendor, Vendor>();

            CreateMap<CreateVendorModel, Services.Vendors.Models.CreateVendorModel>()
                .ForMember(dest => dest.ImageUrls, opt => opt.Ignore());
            CreateMap<UpdateVendorModel, Services.Vendors.Models.UpdateVendorModel>()
                .ForMember(dest => dest.ImageUrls, opt => opt.Ignore());
        }
    }
}
