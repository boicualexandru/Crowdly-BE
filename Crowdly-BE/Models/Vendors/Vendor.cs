using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Crowdly_BE.Models.Vendors
{
    public class Vendor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public double Price { get; set; }
        public string Thumbnail { get; set; }
        public VendorCategoryType Category { get; set; }
    }

    public class VendorsFilters
    {
        public string City { get; set; }
        public VendorCategoryType Category { get; set; }
        public double? PriceMin { get; set; }
        public double? PriceMax { get; set; }
        public int? Guests { get; set; }
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
        public int? GuestsMin { get; set; }
        public int? GuestsMax { get; set; }
        public string[] Images { get; set; }
        public bool IsEditable { get; set; }
        public VendorCategoryType Category { get; set; }
    }

    public class CreateVendorModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public double Price { get; set; }
        public int? GuestsMin { get; set; }
        public int? GuestsMax { get; set; }
        public IFormFile[] FormFiles { get; set; }
        [Required]
        public VendorCategoryType Category { get; set; }
    }

    public class UpdateVendorModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public double Price { get; set; }
        public int? GuestsMin { get; set; }
        public int? GuestsMax { get; set; }
        public IFormFile[]? FormFiles { get; set; }
        public string[] ExistingImages { get; set; }
        [Required]
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
