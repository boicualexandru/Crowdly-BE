using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Crowdly_BE.Models.Events
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
    }

    public class EventsFilters
    {
        public Guid? CityId { get; set; }
        public EventCategoryType Category { get; set; }
        public double? PriceMin { get; set; }
        public double? PriceMax { get; set; }
        public DateTime? AfterDateTime { get; set; }
        public int? Skip { get; set; }
    }

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
        public bool IsEditable { get; set; }
        public EventCategoryType Category { get; set; }
    }

    public class CreateEventModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid CityId { get; set; }
        [Required]
        public double Price { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int? Guests { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public IFormFile[] FormFiles { get; set; }
        [Required]
        public EventCategoryType Category { get; set; }
    }

    public class UpdateEventModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid CityId { get; set; }
        [Required]
        public double Price { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int? Guests { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public IFormFile[]? FormFiles { get; set; }
        public string[] ExistingImages { get; set; }
        [Required]
        public EventCategoryType Category { get; set; }
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
