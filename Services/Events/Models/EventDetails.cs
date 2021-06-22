using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Events.Models
{
    public class EventDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CityId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int? Guests { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool IsPublic { get; set; }
        public string[] Images { get; set; }
        public Guid CreatedByUserId { get; set; }
        public EventCategoryType Category { get; set; }
    }
}
