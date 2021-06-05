﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }

        public List<Vendor> Vendors { get; set; }
        public List<SchedulePeriod> SchedulePeriodsBooked { get; set; }
    }
}
