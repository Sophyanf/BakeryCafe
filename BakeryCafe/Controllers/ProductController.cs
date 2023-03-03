﻿using BakeryCafe.Model;
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
        public async Task<bool> EditProductAsync(Product product, CategoryBakery category, Manufacturer manuf)
        {
            try
            {
                if (category != null)
                {
                    CategoryBakery oldCategory = _context.Products.Include("CategoryBakerys").FirstOrDefault(p => p.ID == product.ID).CategoryBakerys;
                    //_context.CategoryBakeries.Include("Products").FirstOrDefault(c => c.ID == category.ID).Products.Remove(product);
                    _context.CategoryBakeries.Include("Products").FirstOrDefault(c => c.ID == category.ID).Products.Add(product);
                    await _context.SaveChangesAsync();
                }
                if (manuf != null)
                {
                    Manufacturer oldManuf = _context.Products.Include("Manufacturers").FirstOrDefault(p => product.ID == p.ID).Manufacturers.FirstOrDefault();
                    _context.Manufacturer.Include("Products").FirstOrDefault(c => c.ID == oldManuf.ID).Products.Remove(product);
                    _context.Manufacturer.Include("Products").FirstOrDefault(c => c.ID == manuf.ID).Products.Add(product);
                    await _context.SaveChangesAsync();
                }
                //if (_context != null) { await _context.SaveChangesAsync(); }
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async void RemoveProduct (Product prod)
        {
            var result =  _context.Products.FirstOrDefault(p => p.ID== prod.ID);
            _context.Products.Remove(result);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Product>> GetListProductAsync(string DataName)
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
                        _context.Products.Include("CategoryBakeries").ToList(); 
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

        public async Task<string> GetProdManufAsync(Product prod)
        {
            string result = null;

            await Task.Run(() =>
            {
                result = _context.Products.Include("Manufacturers").FirstOrDefault(p => prod.ID == p.ID).Manufacturers.FirstOrDefault().ManufacturerName;
            });
            return result;
        }

        /* public async Task<String> GetProductManufAsync(Product product)
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
         }*/
        public List<Product> load(String category, String manuf)
        {
            if (category == "")
                    return _context.Products
                    .Include("Manufacturers")
                    .Where(p => p.Manufacturers.Any(m => m.ManufacturerName.Contains(manuf)))
                    .ToList();

            return _context.Products
                    .Include("CategoryBakerys")
                    .Include("Manufacturers")
                    .Where(p => p.Manufacturers.Any(m => m.ManufacturerName.Contains(manuf)) && p.CategoryBakerys.CategoryName == category)
                    .ToList();
        }


        public DateTime dateOfProduct()
        {
            Thread.Sleep(10);
            return DateTime.Today.AddDays(-(new Random().Next(0, 5)));
        }

       /* public async void SaveProduct(Product prod)
        {
            var oldProd =_context.Products
                .Include("CategoryBakeries")
                .Include("Manufacturers")
                .FirstOrDefault(p => p.ID == prod.ID);
            oldProd = prod;
            _context.SaveChanges();
        }*/
    }
} 