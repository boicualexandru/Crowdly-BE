using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowdly_BE.Models.SchedulePeriods
{
    public class SchedulePeriod
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid VendorId { get; set; }
        public Guid? BookedByUserId { get; set; }
    }

    public class CreateSchedulePeriodModel
    {
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
