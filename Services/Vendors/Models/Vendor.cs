using System;

namespace Services.Vendors.Models
{
    public class Vendor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public double Price { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
