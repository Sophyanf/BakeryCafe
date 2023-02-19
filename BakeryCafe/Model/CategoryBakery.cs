using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryCafe.Model
{
    public class CategoryBakery : IDataProduct
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public decimal AveragePrice { get; set; }   
        public ICollection<Product> Products { get; set; }
    }
}
