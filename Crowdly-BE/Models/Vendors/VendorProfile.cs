using AutoMapper;
using Crowdly_BE.Models.Common;

namespace Crowdly_BE.Models.Vendors
{
    public class VendorProfile : Profile
    {
        public VendorProfile()
        {
            CreateMap<Services.Vendors.Models.Vendor, Vendor>();
            CreateMap<Services.Common.Models.DataPage<Services.Vendors.Models.Vendor>, DataPage<Vendor>>();
            CreateMap<VendorsFilters, Services.Vendors.Models.VendorsFilters>();

            CreateMap<Services.Vendors.Models.VendorDetails, VendorDetails>();

            CreateMap<CreateVendorModel, Services.Vendors.Models.CreateVendorModel>()
                .ForMember(dest => dest.Images, opt => opt.Ignore());
            CreateMap<UpdateVendorModel, Services.Vendors.Models.UpdateVendorModel>()
                .ForMember(dest => dest.Images, opt => opt.Ignore());
        }
    }
}
