using System;

namespace Services.Vendors.Models
{
    public class UpdateVendorModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public double Price { get; set; }
        public int? GuestsMin { get; set; }
        public int? GuestsMax { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string[] Images { get; set; }
        public VendorCategoryType Category { get; set; }
    }
}
