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
        public VendorCategoryType Category { get; set; }
    }

    public class VendorsFilters
    {
        public string City { get; set; }
        public VendorCategoryType Category { get; set; }
        public int? Skip { get; set; }
    }

    public class VendorDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string[] Images { get; set; }
        public bool IsEditable { get; set; }
        public VendorCategoryType Category { get; set; }
    }

    public class CreateVendorModel
    {
        public string Name { get; set; }
        public string City { get; set; }
        public double Price { get; set; }
        public IFormFile[] FormFiles { get; set; }
        public VendorCategoryType Category { get; set; }
    }

    public class UpdateVendorModel
    {
        public string Name { get; set; }
        public string City { get; set; }
        public double Price { get; set; }
        public IFormFile[]? FormFiles { get; set; }
        public string[] ExistingImages { get; set; }
        public VendorCategoryType Category { get; set; }
    }

    public enum VendorCategoryType
    {
        None = 0,
        Location = 1,
        Music = 2,
        Photo = 3,
        Video = 4,
        Food = 5,
    }
}
