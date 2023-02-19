using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryCafe.Model
{
    public class Manufacturer
    {
        public int ID { get; set; }
        public string ManufacturerName { get; set; }
        public decimal AveragePriceMan { get; set; }
        public ICollection<Product> Products { get; set; }
        public CategoryBakery CategoryBakery { get; set; }
    }
}
