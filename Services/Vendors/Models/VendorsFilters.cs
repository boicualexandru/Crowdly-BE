using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Vendors.Models
{
    public class VendorsFilters
    {
        public string City { get; set; }
        public VendorCategoryType Category { get; set; }
        public double? PriceMin { get; set; }
        public double? PriceMax { get; set; }
        public int? Guests { get; set; }
        public int? Skip { get; set; }
    }
}
