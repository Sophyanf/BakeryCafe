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
    internal class ReportsController
    {
        private readonly AppDbContext _context;
        private DataProductController data = DataProductController.Instance;
        private ProductController dataProduct = ProductController.Instance;
        public static ReportsController Instance { get => ReportsControllerCreate.instance; }
        private ReportsController()
        {
            _context = new AppDbContext();
        }
        private class ReportsControllerCreate
        {
            static ReportsControllerCreate() { }
            internal static readonly ReportsController instance = new ReportsController();
        }

        public async Task<decimal> ManufFromAllProducts(string DataName)  // Получить список продуктов
        {
            int count = dataProduct.load("",DataName).Count;
            int countProd = await dataProduct.GetCountProductAsync();
            return (decimal)count / countProd;
        }
        public async Task<string> ManufFromOtherManuf(string DataName)  // Получить список продуктов
        {
            int countProd = await dataProduct.GetCountProductAsync();
            List<decimal> manufRating = new List<decimal>();
            //string rezInfo = "";
            decimal rezDec = 0;
            string rez = "";
            List<Manufacturer> manufacturers = await data.GetListManufAsync();
            foreach (Manufacturer manufacturer in manufacturers)
            {
                int count = dataProduct.load("", manufacturer.ManufacturerName).Count;
                rez += manufacturer.ManufacturerName + " - " + ((decimal)count/countProd).ToString("0.##") + Environment.NewLine;
                manufRating.Add((decimal)count / countProd);
                if (manufacturer.ManufacturerName == DataName) { rezDec = (decimal)count / countProd; }
            }
            manufRating.Sort();
            manufRating.Reverse();
            int index = manufRating.IndexOf(rezDec);

            rez +=  DataName + " Занимает " + (index + 1) + " место ";
            return rez;
        }

        public async Task<string> expensiveManufPrice(string DataName, decimal priceControl)  // дорогие товары прозводителя
        {
            List<Product> prod = dataProduct.load("", DataName);
            int countProd = prod.Count;
            int countRez = prod.Where(p => p.price > priceControl).Count();
            string rez = "";
            if (countProd == 0) { rez = "Нет товаров данного производителя в данный момент"; }
            else if (countRez == 0) { rez = "Нет товаров данного производителя дороже заданной стоимости"; }
            else { rez = ((decimal)countRez / countProd).ToString(("0.##")); }
            { }
            return rez;
        }

        public async Task<string> cheaperManufPrice(string DataName, decimal priceControl)  // дешевые товары прозводителя
        {
            List<Product> prod  = dataProduct.load("", DataName);
            int countProd = prod.Count;
            int countRez = prod.Where(p => p.price < priceControl).Count();
            string rez = "";
            if (countProd == 0) { rez = "Нет товаров данного производителя в данный момент"; }
            else if (countRez == 0) { rez = "Нет товаров данного производителя дешевле заданной стоимости"; }
            else { rez = ((decimal)countRez / countProd).ToString("0.##"); } { }
            return rez;
        }

        public async Task<string> getPriceWithinLimits (decimal minPrice, decimal maxPrice)  // цена в заданых пределах
        {
            var prodList = await dataProduct.GetListProductAsync("");
            int countProd = prodList.Where(p => p.price >= minPrice && p.price <= maxPrice).Count();
            return ((decimal)countProd / prodList.Count).ToString("0.##");
        }
        public async Task<string> getPriceChipper(decimal maxPrice)  // цена ниже заданой
        {
            var prodList = await dataProduct.GetListProductAsync("");
            int countProd = prodList.Where(p =>p.price < maxPrice).Count();
            return ((decimal)countProd / prodList.Count).ToString("0.##");
        }

        public async Task<string> getDateWithinLimits(DateTime minPrice, DateTime maxPrice)  // цена в заданых пределах
        {
            var prodList = await dataProduct.GetListProductAsync("");
            int countProd = prodList.Where(p => p.dateOfManuf >= minPrice && p.dateOfManuf <= maxPrice).Count();
            return ((decimal)countProd / prodList.Count).ToString("0.##");
        }
    }
}
