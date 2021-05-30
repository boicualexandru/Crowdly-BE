using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SchedulePeriods.Models
{
    public class CreateSchedulePeriodModel
    {
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid VendorId { get; set; }
        public string? BookedByUserId { get; set; }
    }
}
