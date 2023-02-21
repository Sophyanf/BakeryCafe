using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryCafe.Model
{
    public class Product
    {
        public int ID { get; set; }
        public string productName { get; set; }
        public int weight { get; set; }
        public decimal price { get; set; }
       //public CategoryBakery CategoryBakerys { get; set; }
        public ICollection<Manufacturer> Manufacturers { get; set; }
    }

}
