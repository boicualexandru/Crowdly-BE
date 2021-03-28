using Services.Vendors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Services.Vendors.Mappers
{
    class VendorProfile : Profile
    {
        public VendorProfile()
        {
            CreateMap<DataAccess.Models.Vendor, Vendor>();
            CreateMap<Vendor, DataAccess.Models.Vendor>();

            CreateMap<AddVendorModel, DataAccess.Models.Vendor>();
        }
    }
}
