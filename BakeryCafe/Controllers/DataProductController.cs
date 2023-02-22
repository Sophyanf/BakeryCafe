using BakeryCafe.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public async Task<IDataProduct> CheckDataProductAsync(string name, string dataName)
        {
            IDataProduct result = null;
            switch (dataName)
            {
                case "categoryBakery":
                    await Task.Run(() =>
                    {
                        result = _context.CategoryBakeries.Include("Products").FirstOrDefault(s => s.CategoryName == name);
                    });
                    return result; 

                case "manufacturer":
                    await Task.Run(() =>
                    {
                        result = _context.Manufacturer.Include("Products").FirstOrDefault(s => s.ManufacturerName == name);
                    });
                    return result;

                    default: return result;
            }
            
        }
        public async Task<List<IDataProduct>> GetDataProductAsync(string dataName)
        {
            List<IDataProduct> result = null;
            switch (dataName)
            {
                case "categoryBakery":
                    
                    await Task.Run(() =>
                    {
                       List <CategoryBakery> category = null;
                        category = _context.CategoryBakeries.Include("Products").ToList();
                    });
                    return result;

                case "manufacturer":
                    await Task.Run(() =>
                    {
                        result = _context.Manufacturer.Include("Products").ToList();
                    });
                    return null;

                default:
                    return result;
            }
          
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
