using ShopCET47.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCET47.Web.Data.Repositories
{
    interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IQueryable<Order>> GetOrderAsync(string username);
    }
}
