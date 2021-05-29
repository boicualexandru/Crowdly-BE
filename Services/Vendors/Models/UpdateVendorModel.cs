﻿using System;

namespace Services.Vendors.Models
{
    public class UpdateVendorModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public double Price { get; set; }
        public string[] Images { get; set; }
        public VendorCategoryType Category { get; set; }
    }
}
