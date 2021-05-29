using System;

namespace Services.Vendors.Models
{
    public class Vendor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public double Price { get; set; }
        public string Thumbnail { get; set; }
        public string CreatedByUserId { get; set; }
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
