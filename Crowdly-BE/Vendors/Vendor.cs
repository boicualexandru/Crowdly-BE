using Microsoft.AspNetCore.Http;
using System;

namespace Crowdly_BE.Vendors
{
    public class Vendor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public double Price { get; set; }
        public string ThumbnailUrl { get; set; }
    }

    public class VendorDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public double Price { get; set; }
        public string[] ImageUrls { get; set; }
    }

    public class CreateVendorModel
    {
        public string Name { get; set; }
        public string City { get; set; }
        public double Price { get; set; }
        public IFormFile[] FormFiles { get; set; }
    }

    public class UpdateVendorModel
    {
        public string Name { get; set; }
        public string City { get; set; }
        public double Price { get; set; }
        public IFormFile[]? FormFiles { get; set; }
        public string[] ExistingImageUrls { get; set; }
    }
}
