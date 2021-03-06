using System;

namespace Services.Vendors.Models
{
    public class VendorDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CityId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int? GuestsMin { get; set; }
        public int? GuestsMax { get; set; }
        public string[] Images { get; set; }
        public Guid CreatedByUserId { get; set; }
        public VendorCategoryType Category { get; set; }
    }
}
