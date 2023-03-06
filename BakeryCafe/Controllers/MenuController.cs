using BakeryCafe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BakeryCafe.Controllers
{
    internal class MenuController
    {
        private readonly AppDbContext _context;
        private DataProductController data = DataProductController.Instance;
        private ProductController dataProduct = ProductController.Instance;
        public static MenuController Instance { get => MenuControllerCreate.instance; }
        private MenuController()
        {
            _context = new AppDbContext();
        }
        private class MenuControllerCreate
        {
            static MenuControllerCreate() { }
            internal static readonly MenuController instance = new MenuController();
        }


        public async Task<string> GetListCategoryStringAsync()        //Список категорий для lABLE
        {
            string result = "";
            (await data.GetListCategoryAsync()).ForEach(c => result += c.ToString());
            return result;
        }
        
        public async Task<string> GetCategoryStringAsync(string categoryName)        //Категорий для lABLE
        {
            return (await data.GetListCategoryAsync()).FirstOrDefault(c => c.CategoryName == categoryName).ToString(); ;
        }

        public async Task<string> GetCheaperCategoryStringAsync()        //Самая дешевая категория
        {
            List<CategoryBakery> categories = await data.GetListCategoryAsync();
            decimal min = categories.Min(c => c.AveragePrice);
            string rez = "";
            categories.Where(c => c.AveragePrice == min).ToList().ForEach(c => rez += c.ToString());
            return rez;
        }
        public async Task<string> GetExpenciveCategoryStringAsync()        //Самая дорогая категория
        {
            List<CategoryBakery> categories = await data.GetListCategoryAsync();
            decimal max = categories.Max(c => c.AveragePrice);
            string rez = "";
            categories.Where(c => c.AveragePrice == max).ToList().ForEach(c => rez += c.ToString());
            return rez;
        }

        public List<Product> GetSortProductsList(string categoriname, int radioButNum)
        {
            List<Product> rez = dataProduct.load(categoriname, "");

            switch(radioButNum)
            {
                case 0:
                    rez = rez.OrderBy(p => p.productName).ToList();
                    break;
                case 1:
                    rez = rez.OrderBy(p => p.weight).ToList();
                    break;
                case 2:
                    rez = rez.OrderBy(p => p.price).ToList();
                    break;
                case 3:
                    rez = rez.OrderBy(p => p.dateOfManuf).ToList();
                    break;
                default: break;
            }
            return rez;
        }
    }
}

