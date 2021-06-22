using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<SchedulePeriod> SchedulePeriods { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<City>().HasData(new City { Id = new Guid("D6744B0E-B8D8-4D1E-93B8-31ECCD3F9472"), Name = "Cluj-Napoca", County = "Cluj", Latitude = 46.7834818, Longitude = 23.5464725 });
            builder.Entity<City>().HasData(new City { Id = new Guid("8D681E75-755C-4629-A6E9-3F8DADB6A355"), Name = "Iași", County = "Iași", Latitude = 47.1562327, Longitude = 27.5169304 });
            builder.Entity<City>().HasData(new City { Id = new Guid("81D1E28E-915F-41F3-8514-C739CCFDACFB"), Name = "București", County = "București", Latitude = 44.4379853, Longitude = 25.9545531 });
            builder.Entity<City>().HasData(new City { Id = new Guid("00C3300F-FA1F-41B7-ACE2-92BF08E67D06"), Name = "Constanța", County = "Constanța", Latitude = 44.1812034, Longitude = 28.4899218 });
            builder.Entity<City>().HasData(new City { Id = new Guid("A849F99C-49CF-401C-A586-47C22A9EF8A3"), Name = "Timișoara", County = "Timișoara", Latitude = 45.741163, Longitude = 21.1465498 });
            builder.Entity<City>().HasData(new City { Id = new Guid("95C39EE9-FE71-4CC5-AE6F-B4F8BDA8495A"), Name = "Brașov", County = "Brașov", Latitude = 45.6525767, Longitude = 25.5264228 });
        }
    }
}
