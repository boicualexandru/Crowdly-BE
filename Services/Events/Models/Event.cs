using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Events.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CityId { get; set; }
        public double Price { get; set; }
        public string Thumbnail { get; set; }
        public EventCategoryType Category { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string CreatedByUserId { get; set; }
    }

    public enum EventCategoryType
    {
        None = 0,
        Party = 1,
        Music = 2,
        Comedy = 3,
        Art = 4,
        Lifestyle = 5,
        Comunity = 6,
        Corporate = 7,
        Personal = 8,
    }
}
