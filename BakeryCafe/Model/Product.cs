using BakeryCafe.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BakeryCafe.Model
{
    public class Product
    {

        private DataProductController data = DataProductController.Instance;
        private ProductController dataProduct = ProductController.Instance;
        public static string manufData = "manufacturer";
        public static string categoryData = "categoryBakery";
        public int ID { get; set; }
        public string productName { get; set; }
        public int weight { get; set; }
        public decimal price { get; set; }
        public DateTime dateOfManuf { get; set; }
        public CategoryBakery CategoryBakerys { get; set; }
        public  ICollection<Manufacturer> Manufacturers { get; set; } // попробовать еще

       
        public override string ToString()
        {
            var mans = "";
            foreach (var manufacturer in Manufacturers)
            {
                mans += manufacturer.ToString();
            }
            return $"{productName} Вес: {weight} Цена: {price} Производитель: {mans}";
        }
       
    }

   
}

