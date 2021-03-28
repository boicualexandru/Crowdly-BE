using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Seed
{
    public static class DataInitializer
    {
        public static async Task SeedData(ApplicationDbContext dbContext)
        {
            dbContext.Users.Add(new Models.ApplicationUser
            {
                
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
