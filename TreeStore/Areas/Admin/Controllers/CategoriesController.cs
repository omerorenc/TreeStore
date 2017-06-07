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

namespace TreeStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        public CategoriesController(ICategoryService categoryService, IProductService _prodcutService)
        {
            this.categoryService = categoryService;
            this.productService = _prodcutService;
        }

 
        public IActionResult Index()
        {
           
            return View(categoryService.GetCategories());
        }

        // GET: Admin/Categories/Details/5
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = categoryService.GetCategories().AsQueryable().Include(c => c.Products).Include(c => c.CategoryCampaign).FirstOrDefault(p => p.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
            
            //ViewData["ProductId"] = new SelectList(productService.GetProducts(), "Id", "Name");
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryService.CreateCategory(category);
                categoryService.SaveCategory();
                return RedirectToAction("Index");
            }

           
            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category =categoryService.GetCategories().AsQueryable().Include(c => c.Products).Include(c => c.CategoryCampaign).FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    categoryService.UpdateCategory(category);
                    categoryService.SaveCategory();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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

            
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = categoryService.GetCategories().AsQueryable().Include(c => c.Products).Include(c => c.CategoryCampaign).FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {

            categoryService.DeleteCategory(id);
            categoryService.SaveCategory();
            return RedirectToAction("Index");
        }

        private bool CategoryExists(long id)
        {
            return categoryService.GetCategories().Any(e => e.Id == id);
        }
    }
}
