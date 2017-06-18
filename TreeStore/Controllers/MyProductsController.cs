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
using Microsoft.AspNetCore.Authorization;

namespace TreeStore.Controllers
{
    [Authorize(Roles = "Firma Sahibi")]
    public class MyProductsController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly ISliderService sliderService;

        public MyProductsController(IProductService _productService, ICategoryService _categoryService, ISliderService _sliderService)
        {
            this.productService = _productService;
            this.categoryService = _categoryService;
            this.sliderService = _sliderService;
        }

        // GET: MyProducts
        public IActionResult Index()
        {
            var applicationDbContext = productService.GetProducts().AsQueryable().Include(p => p.Category).Include(p => p.Slider).Where(p => p.CreatedBy == User.Identity.Name);
            return View(applicationDbContext.ToList());
        }

        // GET: MyProducts/Details/5
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = productService.GetProducts().AsQueryable()
                .Include(p => p.Category)
                .Include(p => p.Slider)
                .Where(p => p.CreatedBy == User.Identity.Name)
                .SingleOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: MyProducts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(categoryService.GetCategories().Where(c => c.ParentCategoryId != null), "Id", "Name");
            ViewData["SliderId"] = new SelectList(sliderService.GetSliders(), "Id", "Name");
            return View();
        }

        // POST: MyProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
            {
                product.CreateDate = DateTime.Now;
                product.UpdateDate = DateTime.Now;
                product.CreatedBy = User.Identity.Name;
                product.UpdateBy = User.Identity.Name;
                productService.CreateProduct(product);
                productService.SaveProduct();
                return RedirectToAction("Index");
            }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Create");
                throw ex; 
               

            }
            ViewData["CategoryId"] = new SelectList(categoryService.GetCategories(), "Id", "Name", product.CategoryId);
            ViewData["SliderId"] = new SelectList(sliderService.GetSliders(), "Id", "Name", product.SliderId);
            return View(product);
        }

        // GET: MyProducts/Edit/5
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = productService.GetProducts().Where(p => p.CreatedBy == User.Identity.Name).SingleOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(categoryService.GetCategories().Where(c => c.ParentCategoryId != null), "Id", "Name", product.CategoryId);
            ViewData["SliderId"] = new SelectList(sliderService.GetSliders(), "Id", "Name", product.SliderId);
            return View(product);
        }

        // POST: MyProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id,Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   
                    product.UpdateDate = DateTime.Now;
                  
                    product.UpdateBy = User.Identity.Name;
                    productService.UpdateProduct(product);
                    productService.SaveProduct();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["CategoryId"] = new SelectList(categoryService.GetCategories(), "Id", "Name", product.CategoryId);
            ViewData["SliderId"] = new SelectList(sliderService.GetSliders(), "Id", "Name", product.SliderId);
            return View(product);
        }

        // GET: MyProducts/Delete/5
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = productService.GetProducts().AsQueryable()
                .Include(p => p.Category)
                .Include(p => p.Slider)
                .Where(p => p.CreatedBy == User.Identity.Name)
                .SingleOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: MyProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            
            productService.DeleteProduct(id);
            productService.SaveProduct();
            return RedirectToAction("Index");
        }

        private bool ProductExists(long id)
        {
            return productService.GetProducts().Where(p => p.CreatedBy == User.Identity.Name).Any(e => e.Id == id);
        }
    }
}
