﻿using ShopCET47.Web.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopCET47.Web.Data.Repositories
{
    public interface IRepository
    {
        void AddProduct(Product product);

        Product GetProduct(int id);

        IEnumerable<Product> GetProducts();

        bool ProductExists(int Id);

        void RemoveProduct(Product product);

        Task<bool> SaveAllAsync();

        void UpdateProduct(Product product);
    }
}