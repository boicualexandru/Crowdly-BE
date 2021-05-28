using AutoMapper;

namespace Crowdly_BE.Vendors
{
    public class VendorProfile : Profile
    {
        public VendorProfile()
        {
            CreateMap<Services.Vendors.Models.Vendor, Vendor>();
            CreateMap<Services.Vendors.Models.DataPage<Services.Vendors.Models.Vendor>, DataPage<Vendor>>();
            CreateMap<VendorsFilters, Services.Vendors.Models.VendorsFilters>();

            CreateMap<Services.Vendors.Models.VendorDetails, VendorDetails>();

            CreateMap<CreateVendorModel, Services.Vendors.Models.CreateVendorModel>()
                .ForMember(dest => dest.ImageUrls, opt => opt.Ignore());
            CreateMap<UpdateVendorModel, Services.Vendors.Models.UpdateVendorModel>()
                .ForMember(dest => dest.ImageUrls, opt => opt.Ignore());
        }
    }
}
