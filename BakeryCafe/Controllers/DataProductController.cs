using BakeryCafe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryCafe.Controllers
{
    internal class DataProductController : IDataProduct
    {
        private readonly AppDbContext _context;
        public static DataProductController Instance { get => DataProdactControllerCreate.instance; }
        private DataProductController()
        {
            _context = new AppDbContext();
        }
        private class DataProdactControllerCreate
        {
            static DataProdactControllerCreate() { }
            internal static readonly DataProductController instance = new DataProductController();
        }

        public async Task<CategoryBakery> GetCategoryAsync(string name)
        {
            CategoryBakery result = null;
            await Task.Run(() =>
            {
                result = _context.CategoryBakeries.Include("Products").FirstOrDefault(s => s.CategoryName == name);
            });
            return result;
        }
        public async Task<List<CategoryBakery>> GetCategoryBakeryAsync()
        {
            List<CategoryBakery> result = null;
            await Task.Run(() =>
            {
                result = _context.CategoryBakeries.Include("Products").ToList();
            });
            return result;
        }
        public async Task<List<Manufacturer>> GetManufAsync()
        {
            List<Manufacturer> result = null;
            await Task.Run(() =>
            {
                result = _context.Manufacturer.Include("Services").ToList();
            });
            return result;
        }

        public async Task<bool> AddCategoryAsync(CategoryBakery category)
        {
            try
            {
                _context.CategoryBakeries.Add(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
