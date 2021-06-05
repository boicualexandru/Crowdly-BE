using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SchedulePeriods.Models
{
    public class UserSchedulePeriod
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public VendorDetails Vendor { get; set; }
        public Guid? BookedByUserId { get; set; }
    }
}
