using BakeryCafe.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        public async Task<IDataProduct> GetProductCategoryAsync(string dataName, string dataType)
        {
            IDataProduct result = null;
            switch (dataType)
            {
                case "categoryBakery":

                    await Task.Run(() =>
                    {
                        result = _context.CategoryBakeries.Include("Products").FirstOrDefault(s => s.CategoryName == dataName);
                        //result = _context.CategoryBakeries.Include("Products").Include("Services.Registrations").FirstOrDefault(s => s.CategoryName == categoryName);

                    });
                    return result;

                case "manufacturer":
                    await Task.Run(() =>
                    {
                        result = _context.Manufacturer.Include("Products").FirstOrDefault(s => s.ManufacturerName == dataName);
                    });
                    return result;

                default:
                    return result;
            }
        }

        public async Task<List<CategoryBakery>> GetCategoryAsync()
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
                result = _context.Manufacturer.Include("Products").ToList();
            });
            return result;
        }

        public async Task<string> GetProdManufAsync(Product prod)
        {
            string result = null;
          
            await Task.Run(() =>
            {
                _context.Products.Include("Manufacturers").ToList();
                 //.FirstOrDefault(p => prod.ID == p.ID).Manufacturers.
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
        public async Task<bool> AddDataProdAsync(IDataProduct obj)
        {
            try
            {
                if (obj is CategoryBakery)
                {
                    var category = obj as CategoryBakery;
                    _context.CategoryBakeries.Add(category);
                }
                else if (obj is Manufacturer)
                {
                    var manufacturer = obj as Manufacturer;
                    _context.Manufacturer.Add(manufacturer);
                }

                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }
        /*   public async Task<List<IDataProduct>> GetDataProductAsync(string dataType) // вместо GetCategoryAsync() и GetManufAsync()
           {
               List<IDataProduct> result = null;

               switch (dataType)
               {
                   case "categoryBakery":

                       await Task.Run(() =>
                       {
                           result = (CategoryBakery)_context.CategoryBakeries.Include("Products").ToList(); // c распаковкой тоже не получается
                           //result = _context.CategoryBakeries.Include("Products").Include("Services.Registrations").FirstOrDefault(s => s.CategoryName == categoryName);

                       });
                       return result;

                   case "manufacturer":
                       await Task.Run(() =>
                       {
                           result = _context.CategoryBakeries.Include("Products").ToList();
                       });
                       return result;

                   default:
                       return result;
               }
           }*/
    }
      
}
