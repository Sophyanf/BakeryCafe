using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BakeryCafe.Model
{
    internal class AddDbContext : DbContext
    {
        public AddDbContext () : base("DefaultConnection")
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<CategoryBakery> Categories { get; set; }
        public DbSet<Manufacturer>  Manufacturer { get; set; }
    }
}
