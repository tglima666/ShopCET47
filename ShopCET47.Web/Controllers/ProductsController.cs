using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopCET47.Web.Data;
using ShopCET47.Web.Data.Entities;
using ShopCET47.Web.Data.Repositories;
using ShopCET47.Web.Helpers;
using ShopCET47.Web.Models;

namespace ShopCET47.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserHelper _userHelper;

        public ProductsController(IProductRepository productRepository, IUserHelper userHelper)
        {
            _productRepository = productRepository;
            _userHelper = userHelper;
        }

        

        // GET: Products
        public IActionResult Index()
        {
            return View(_productRepository.GetAll());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetByIDAsync(id.Value);
                
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Price,ImageFile,LastPurchase,LastSale,IsAvailable,Stock")] ProductViewModel view)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (view.ImageFile != null && view.ImageFile.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";

                    path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\Products",
                        file);

                    using(var stream = new FileStream(path, FileMode.Create))
                    {
                        await view.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/Products/{file}";
                }

                var product = this.ToProduct(view, path);

                //TODO: Mudar para o user que depois tiver logado
                product.User = await _userHelper.GetUserByEmailAsync("tiago.sa.lima@formandos.cinel.pt");
                await _productRepository.CreateAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }

        private Product ToProduct(ProductViewModel view, string path)
        {
            return new Product
            {
                ID = view.ID,
                ImageURL = path,
                IsAvailable = view.IsAvailable,
                LastPurchase = view.LastPurchase,
                LastSale = view.LastSale,
                Name = view.Name,
                Price = view.Price,
                Stock = view.Stock,
                User = view.User
            };
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetByIDAsync(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            var view = this.ToProductViewModel(product);

            return View(view);
        }

        private ProductViewModel ToProductViewModel(Product product)
        {
            return new ProductViewModel
            {
                ID = product.ID,
                ImageURL = product.ImageURL,
                IsAvailable = product.IsAvailable,
                LastPurchase = product.LastPurchase,
                LastSale = product.LastSale,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                User = product.User
            };
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Price,ImageFile,LastPurchase,LastSale,IsAvailable,Stock")] ProductViewModel view)
        {
            if (id != view.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var path = view.ImageURL;

                    if (view.ImageFile != null && view.ImageFile.Length > 0)
                    {
                        path = string.Empty;

                        if (view.ImageFile != null && view.ImageFile.Length > 0)
                        {
                            var guid = Guid.NewGuid().ToString();
                            var file = $"{guid}.jpg";

                            path = Path.Combine(
                                Directory.GetCurrentDirectory(),
                                "wwwroot\\images\\Products",
                                file);

                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await view.ImageFile.CopyToAsync(stream);
                            }

                            path = $"~/images/Products/{file}";
                        }
                    }

                    var product = this.ToProduct(view, path);

                    //TODO: Mudar para o user que depois tiver logado
                    product.User = await _userHelper.GetUserByEmailAsync("tiago.sa.lima@formandos.cinel.pt");
                    await _productRepository.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _productRepository.ExistAsync(view.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetByIDAsync(id.Value);
                
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productRepository.GetByIDAsync(id);
            await _productRepository.DeleteAsync(product);
            return RedirectToAction(nameof(Index));
        }        
    }
}
