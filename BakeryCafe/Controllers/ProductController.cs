﻿using BakeryCafe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryCafe.Controllers
{
    internal class DataProdactController
    {
        private readonly AppDbContext _context;
        public static DataProdactController Instance { get => ProductControllerCreate.instance; }
        private DataProdactController()
        {
            _context = new AppDbContext();
        }
        private class ProductControllerCreate
        {
            static ProductControllerCreate() { }
            internal static readonly DataProdactController instance = new ProductController();
        }

        public async Task<bool> AddClient(Client client, Manager manager)
        {
            _context.Managers.Include("Clients").FirstOrDefault(m => m.Id == manager.Id).Clients.Add(client);
            int res = await _context.SaveChangesAsync();
            if (res == 0)
            {
                return false;
            }
            return true;
        }
    }
}
