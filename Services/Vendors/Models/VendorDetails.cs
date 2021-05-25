﻿using System;

namespace Services.Vendors.Models
{
    public class VendorDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public double Price { get; set; }
        public string[] ImageUrls { get; set; }
        public string CreatedByUserId { get; set; }
        public VendorCategoryType Category { get; set; }
    }
}
