using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowdly_BE.Vendors
{
    public class Vendor
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public double Price { get; set; }
        public string ThumbnailUrl { get; set; }
    }

    public class CreateVendorModel
    {
        public string Name { get; set; }
        public string City { get; set; }
        public double Price { get; set; }
        public IFormFile[] FormFiles { get; set; }
    }
}
