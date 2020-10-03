using EventsSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsSystem.Data.Seeding
{
   public class PlacesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Places.Any())
            {
                return;
            }

            var places = new List<string> { "Ancient theatre", "Plovdiv Old Town", "Plovdiv Aviation Museum" };
            foreach (var place in places)
            {
                await dbContext.Places.AddAsync(new Place
                {
                    Name = place,
                    Address = place,
                }); 
            }
        }
    }
}
