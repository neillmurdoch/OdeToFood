using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Models;

namespace OdeToFood.Data
{
    public class OdeToFoodDBContext : DbContext
    {
        // Just pass the options along to the base class.
        public OdeToFoodDBContext(DbContextOptions options)
            : base(options)
        {
            
        }

        public DbSet<Restaurant> Restaurants { get; set; }


    }
}
