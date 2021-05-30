using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Vendor> Vendors { get; set; }
        public List<SchedulePeriod> SchedulePeriodsBooked { get; set; }
    }
}
