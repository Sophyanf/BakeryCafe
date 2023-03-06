﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryCafe.Model
{
    public class CategoryBakery : IDataProduct
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public decimal AveragePrice { get; set; }   
        //public string pathToFile { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Manufacturer> Manufacturers { get; set; }


        public override string ToString()
        {
            return $"{CategoryName} - средняя цена: {AveragePrice}" + Environment.NewLine;
            // return $"{productName}( {manufName}) Дата: {dateOfManuf} Цена: {price} " + Environment.NewLine;
        }
    }
}
