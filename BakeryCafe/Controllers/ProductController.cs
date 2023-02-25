using BakeryCafe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
