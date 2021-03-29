using System;

namespace DataAccess.Models
{
    public class Vendor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public double Price { get; set; }
        public string ThumbnailUrl { get; set; }
        public string[] ImageUrls { get; set; }

        public string CreatedByUserId { get; set; }
        public ApplicationUser CreatedByUser { get; set; }
    }
}
