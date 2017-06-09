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

namespace TreeStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly ISliderService sliderService;

        public ProductsController(IProductService _productService, ICategoryService _categoryService, ISliderService _sliderService)
        {
            this.productService = _productService;
            this.categoryService = _categoryService;
            this.sliderService = _sliderService;
        }

        // GET: Admin/Products
        public IActionResult Index()
        {
            var applicationDbContext = productService.GetProducts().AsQueryable().Include(p => p.Category).Include(p => p.Slider);
            return View(applicationDbContext.ToList());
        }

        // GET: Admin/Products/Details/5
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = productService.GetProducts().AsQueryable()
                .Include(p => p.Category)
                .Include(p => p.Slider)
                .SingleOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(categoryService.GetCategories(), "Id", "Name");
            ViewData["SliderId"] = new SelectList(sliderService.GetSliders(), "Id", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Description,Price,DiscountPrice,ImagePath,IsActive,CompanyLink,CategoryId,SliderId,Id,Name,CreateDate,UpdateDate,CreatedBy,UpdateBy")] Product product)
        {
            if (ModelState.IsValid)
            {
                productService.CreateProduct(product);
                productService.SaveProduct();
                return RedirectToAction("Index");
            }
            ViewData["CategoryId"] = new SelectList(categoryService.GetCategories(), "Id", "Name", product.CategoryId);
            ViewData["SliderId"] = new SelectList(sliderService.GetSliders(), "Id", "Name", product.SliderId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = productService.GetProducts().SingleOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(categoryService.GetCategories(), "Id", "Name", product.CategoryId);
            ViewData["SliderId"] = new SelectList(sliderService.GetSliders(), "Id", "Name", product.SliderId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Description,Price,DiscountPrice,ImagePath,IsActive,CompanyLink,CategoryId,SliderId,Id,Name,CreateDate,UpdateDate,CreatedBy,UpdateBy")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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

        // GET: Admin/Products/Delete/5
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = productService.GetProducts().AsQueryable()
                .Include(p => p.Category)
                .Include(p => p.Slider)
                .SingleOrDefault(m => m.Id == id);
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
            var product = productService.GetProducts().SingleOrDefault(m => m.Id == id);
            productService.DeleteProduct(id);
            productService.SaveProduct();
            return RedirectToAction("Index");
        }

        private bool ProductExists(long id)
        {
            return productService.GetProducts().Any(e => e.Id == id);
        }
    }
}
