using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public class Vendor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public Guid CityId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int? GuestsMin { get; set; }
        public int? GuestsMax { get; set; }
        public string Thumbnail { get; set; }
        public string[] Images { get; set; }
        public VendorCategoryType Category { get; set; }

        public Guid CreatedByUserId { get; set; }
        public ApplicationUser CreatedByUser { get; set; }

        public List<SchedulePeriod> SchedulePeriods { get; set; }
        public City CityRef { get; set; }
    }

    public enum VendorCategoryType
    {
        None = 0,
        Location = 1,
        Music = 2,
        Photo = 3,
        Video = 4,
        Food = 5,
        Entertainment = 6,
        Decoration = 7,
        Flowers = 8,
    }
}
