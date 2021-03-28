using AutoMapper;

namespace Crowdly_BE.Vendors
{
    public class VendorProfile : Profile
    {
        public VendorProfile()
        {
            CreateMap<Services.Vendors.Models.Vendor, Vendor>();
            CreateMap<Vendor, Services.Vendors.Models.Vendor>();

            CreateMap<CreateVendorModel, Services.Vendors.Models.CreateVendorModel>();
        }
    }
}
