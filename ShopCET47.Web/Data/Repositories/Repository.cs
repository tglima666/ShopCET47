using ShopCET47.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCET47.Web.Data.Repositories
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        //Método que vai buscar os produtos todos
        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.OrderBy(p => p.Name);
        }

        //Método que vai buscar um produto pelo id
        public Product GetProduct(int id)
        {
            return _context.Products.Find(id);
        }

        //Método que adiciona um produto à tabela
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

        //Método que actualiza (update) um produto
        public void UpdateProduct(Product product)
        {
            _context.Update(product);
        }

        //Método que remove um produto
        public void RemoveProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        //Método que actualiza a BD
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        //mètodo que verifica se o produto existe
        public bool ProductExists(int id)
        {
            return _context.Products.Any(p => p.ID == id);
        }
    }
}
