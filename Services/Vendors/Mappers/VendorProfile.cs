using Services.Vendors.Models;
using AutoMapper;

namespace Services.Vendors.Mappers
{
    class VendorProfile : Profile
    {
        public VendorProfile()
        {
            CreateMap<DataAccess.Models.Vendor, Vendor>();
            CreateMap<Vendor, DataAccess.Models.Vendor>();

            CreateMap<CreateVendorModel, DataAccess.Models.Vendor>();
        }
    }
}
