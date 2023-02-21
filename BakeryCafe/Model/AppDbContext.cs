using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BakeryCafe.Model
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext () : base("DefaultConnection")
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<CategoryBakery> CategoryBakeries { get; set; }
        public DbSet<Manufacturer>  Manufacturer { get; set; }
    }
}
