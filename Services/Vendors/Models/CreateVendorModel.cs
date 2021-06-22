using System;

namespace Services.Vendors.Models
{
    public class CreateVendorModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CityId { get; set; }
        public double Price { get; set; }
        public int? GuestsMin { get; set; }
        public int? GuestsMax { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string[] Images { get; set; }
        public Guid CreatedByUserId { get; set; }
        public VendorCategoryType Category { get; set; }
    }
}
