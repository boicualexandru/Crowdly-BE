﻿namespace Services.Vendors.Models
{
    public class CreateVendorModel
    {
        public string Name { get; set; }
        public string City { get; set; }
        public double Price { get; set; }
        public string[] ImageUrls { get; set; }
    }
}
