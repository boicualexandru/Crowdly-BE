using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Events.Models
{
    public class EventsFilters
    {
        public Guid? CityId { get; set; }
        public EventCategoryType Category { get; set; }
        public double? PriceMin { get; set; }
        public double? PriceMax { get; set; }
        public DateTime? AfterDateTime { get; set; }
        public int? Skip { get; set; }
    }
}
