using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class SchedulePeriod
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid VendorId { get; set; }
        public Vendor Vendor { get; set; }

        public string? BookedByUserId { get; set; }
        public ApplicationUser BookedByUser { get; set; }
    }
}
