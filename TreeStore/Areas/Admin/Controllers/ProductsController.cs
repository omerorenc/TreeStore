using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TreeStore.Data;
using TreeStore.Models;
using TreeStore.Services;
using System.Collections;
using Microsoft.Extensions.Logging;
using TreeStore.Models.ViewComponents;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace TreeStore.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    public class ProductsController : Controller
    {
        
        
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public ProductsController( IProductService productService, ICategoryService categoryService)
        {
            
            this.productService = productService;
            this.categoryService = categoryService;
        }

        public IActionResult Index()
        {
            
            var products = productService.GetProducts();
            return View(products);
        }


        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = productService.GetProducts().AsQueryable().Include(p=>p.Category).FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = productService.GetProducts().AsQueryable().Include(p => p.Category).FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {

            productService.DeleteProduct(id);
            productService.SaveProduct();
            return RedirectToAction("Index");
        }
    }
}
