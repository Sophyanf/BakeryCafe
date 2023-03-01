using BakeryCafe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BakeryCafe.Controllers
{
    internal class ProductController
    {
        private readonly AppDbContext _context;
        public static ProductController Instance { get => ProductControllerCreate.instance; }
        private ProductController()
        {
            _context = new AppDbContext();
        }
        private class ProductControllerCreate
        {
            static ProductControllerCreate() { }
            internal static readonly ProductController instance = new ProductController();
        }

        public async Task<bool> AddProductAsync(Product product, CategoryBakery category, Manufacturer manuf)
        {
            try
            {
                _context.CategoryBakeries.Include("Products").FirstOrDefault(c => c.ID == category.ID).Products.Add(product);
                _context.Manufacturer.Include("Products").FirstOrDefault(c => c.ID == manuf.ID).Products.Add(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<List<Product>> GetProductAsync(string DataName)
        {
            List<Product> result = null;

            await Task.Run(() =>
            {
                if (DataName == "")
                {
                    result = _context.Products.ToList();
                }
                else
                {
                    foreach (var product in _context.Products)
                    {
                        _context.Products.Include("CategoryBakeries").ToList(); //РАЗОБРАТЬСЯ С ЗАПРОСАМИ
                        result.Add(product);
                    }
                }
            });
            return result;

        }
        public async Task<String> GetProductCategoryAsync(Product product)
        {
            string rez = null;
            await Task.Run(() =>
            {
                rez = _context.Products.Include("CategoryBakerys").FirstOrDefault(p => p.ID == product.ID).CategoryBakerys.CategoryName;
            });
            return rez;
        }

        public async Task<String> GetProductManufAsync(Product product)
        {
            string rez = null;

            await Task.Run(() =>
            {
                var listProdManeuf = _context.Products.Include("Manufacturers").Include("Products.Manufacturers").ToList();
                foreach (var prod in listProdManeuf)
                {
                    //if (prod.ID == product.ID) {prod.Manufacturers.}
                }
            });

            return rez;
        }
        public List<Product> load(String category, String manuf)
        {

            return _context.Products
                .Include("CategoryBakerys")
                .Include("Manufacturers")
                .Where(p => p.Manufacturers.Any(m => m.ManufacturerName.Contains(manuf)) && p.CategoryBakerys.CategoryName == category)
                .ToList();
            /*var query = from p in _context.Set<Product>()
                        join m in _context.Set<Manufacturer>()
                        on m equals p
                        where (category == null || p.CategoryBakerys.CategoryName.Contains(category))
                        
                        select p;
            return query.ToList();*/
        }


        public DateTime dateOfProduct()
        {
            Thread.Sleep(10);
            return DateTime.Today.AddDays(-(new Random().Next(0, 5)));
        }

        public async void SaveProduct(Product prod)
        {
            var oldProd =_context.Products
                .Include("CategoryBakeries")
                .Include("Manufacturers")
                .FirstOrDefault(p => p.ID == prod.ID);
            oldProd = prod;
            _context.SaveChanges();
        }
    }
} 