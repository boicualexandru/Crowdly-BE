using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SchedulePeriods.Models
{
    public class VendorDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
