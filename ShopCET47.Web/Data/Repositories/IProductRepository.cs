using Microsoft.AspNetCore.Mvc.Rendering;
using ShopCET47.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCET47.Web.Data.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IEnumerable<SelectListItem> GetComboProducts();
    }
}
