using BakeryCafe.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics.Eventing.Reader;

namespace BakeryCafe.Controllers
{
    internal class DataProductController : IDataProduct
    {
        private readonly AppDbContext _context;
        public static DataProductController Instance { get => DataProdactControllerCreate.instance; }
        private ProductController dataProduct = ProductController.Instance;


        private DataProductController()
        {
            _context = new AppDbContext();
        }
        private class DataProdactControllerCreate
        {
            static DataProdactControllerCreate() { }
            internal static readonly DataProductController instance = new DataProductController();
        }

        public async Task<decimal> AveragePriceAsync(string dataType, string dataName)  // Расчет средней стоимости (не работает с await)
        {
            List<Product> result = null;
            decimal sumPrice = 0;
            switch (dataType)
            {
                case "categoryBakery":
                    result = _context.Products
                      .Include("CategoryBakerys")
                      .Where(p => p.CategoryBakerys.CategoryName == dataName)
                      .ToList();

                    break;

                case "manufacturer":
                    /* await Task.Run(() =>
                     {*/
                    result = _context.Products
                  .Include("Manufacturers")
                  .Where(p => p.Manufacturers.Any(m => m.ManufacturerName == dataName))
                  .ToList();
                    
                   // });
                      
                    break;

                default: break;
            }
            try
            {
                result.ForEach(p => sumPrice += p.price);
                return sumPrice / result.Count;
            }
            catch { MessageBox.Show("Нет таких продуктов"); return 0; }
        }
        public async Task<bool> AddDataProdAsync(IDataProduct obj)   //добавление категории\производителя
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
       
        public async Task<IDataProduct> CheckDataProductAsync(string name, string dataName) // проверка наличия категории/производителя
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
public async Task<decimal> GetCMinPriceAsync(string manufName)  // Минимальная стоимость
        {
            List<Product> result = null;
            if (manufName == "") { result = await dataProduct.GetListProductAsync(""); }
            else { result = dataProduct.load("", manufName); }
            return result.Min(p => p.price);
        }

        public async Task<decimal> GetCMaxPriceAsync(string manufName)  // Максимальная стоимость
        {
            List<Product> result = null;
            if (manufName == "") { result = await dataProduct.GetListProductAsync(""); }
            else { result = dataProduct.load("", manufName); }
            return result.Max(p => p.price);
        }
        public async Task<DateTime> GetCMinDateAsync(string manufName)  // Минимальная стоимость
        {
            List<Product> result = null;
            if (manufName == "") { result = await dataProduct.GetListProductAsync(""); }
            else { result = dataProduct.load("", manufName); }
            return result.Min(p => p.dateOfManuf);
        }

        public async Task<DateTime> GetCMaxDateAsync(string manufName)  // Максимальная стоимость
        {
            List<Product> result = null;
            if (manufName == "") { result = await dataProduct.GetListProductAsync(""); }
            else { result = dataProduct.load("", manufName); }
            return result.Max(p => p.dateOfManuf);
        }



        /* public async Task<IDataProduct> GetProductDataAsync(string dataName, string dataType)   //
         {
             IDataProduct result = null;
             switch (dataType)
             {
                 case "categoryBakery":

                     await Task.Run(() =>
                     {
                         result = _context.CategoryBakeries.Include("Products").FirstOrDefault(s => s.CategoryName == dataName);

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
         }*/

        public async Task<List<CategoryBakery>> GetListCategoryAsync()          //Список категорий (по возможности заменить на GetDataProductAsync(string dataType) ))
        {
            List<CategoryBakery> result = null;

            await Task.Run(() =>
            {
                result = _context.CategoryBakeries.Include("Products").ToList();
            });
            return result;
        }

        public async Task<List<Manufacturer>> GetListManufAsync()     //Список производителей (по возможности заменить на GetDataProductAsync(string dataType) ))
        {
            List<Manufacturer> result = null;
            await Task.Run(() =>
            {
                result = _context.Manufacturer.Include("Products").ToList();
            });
            return result;
        }
       
         /* public async Task<List<IDataProduct>> GetDataProductAsync(string dataType) // вместо GetCategoryAsync() и GetManufAsync()
           {
              IList result = null;


               switch (dataType)
               {
                   case "categoryBakery":

                       await Task.Run(() =>
                       {
                           result = _context.CategoryBakeries.Include("Products").ToList(); // c распаковкой тоже не получается
                           //result = _context.CategoryBakeries.Include("Products").Include("Services.Registrations").FirstOrDefault(s => s.CategoryName == categoryName);

                       });
                       return (List < IDataProduct>)result;

                   case "manufacturer":
                       await Task.Run(() =>
                       {
                           result = _context.CategoryBakeries.Include("Products").ToList();
                       });
                    return (List<IDataProduct>)result;

                default:
                    return (List<IDataProduct>)result;
                }
          }*/

        public async void fillAverPrice ()  // заполнение средней стоимости (можно удалить)
        {
            var list = await _context.CategoryBakeries.ToListAsync();
            for (int i = 0;i < list.Count; i++)
            {
                list[i].AveragePrice = await AveragePriceAsync("categoryBakery", list[i].CategoryName);
            }
            _context.SaveChanges();
        }
    }
}
