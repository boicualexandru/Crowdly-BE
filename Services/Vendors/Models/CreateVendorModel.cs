﻿using System;

namespace Services.Vendors.Models
{
    public class CreateVendorModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public double Price { get; set; }
        public string[] Images { get; set; }
        public string CreatedByUserId { get; set; }
        public VendorCategoryType Category { get; set; }
    }
}
