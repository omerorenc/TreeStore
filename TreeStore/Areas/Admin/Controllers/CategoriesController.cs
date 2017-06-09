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
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        public CategoriesController(ICategoryService categoryService, IProductService prodcutService)
        {
            this.categoryService = categoryService;
            this.productService = prodcutService;
        }

 
        public IActionResult Index()
        {
            var categories = categoryService.GetCategories().AsQueryable().Include(c => c.ParentCategory).ToList();
           
            return View(categories);
        }

        // GET: Admin/Categories/Details/5
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = categoryService.GetCategories().AsQueryable().Include(c => c.Products).Include(c => c.ParentCategory).Include(c => c.ChildCategories).FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
            ViewBag.ParentCategoryId = new SelectList(categoryService.GetCategories(), "Id", "Name");

            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            Category newCategory;
            if (ModelState.IsValid)
            {
                if(category.ParentCategoryId !=null && category.ParentCategory != null)
                {
                    newCategory = categoryService.GetCategories().FirstOrDefault(c => c.Id == category.ParentCategoryId);
                    newCategory.ChildCategories.Add(category);
                }
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
            ViewBag.ParentCategoryId = new SelectList(categoryService.GetCategories(), "Id", "Name",categoryService.GetCategories().FirstOrDefault(c => c.ParentCategoryId == id));
            var category =categoryService.GetCategories().AsQueryable().Include(c => c.Products).Include(c => c.ParentCategory).Include(c=> c.ChildCategories).FirstOrDefault(c => c.Id == id);
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
        public IActionResult Edit(long id,long? ParentCategoryId, Category category)
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

            var category = categoryService.GetCategories().AsQueryable().Include(c => c.Products).Include(c => c.CategoryCampaign)
                .Include(c => c.ParentCategory)
                .Include(c => c.ChildCategories)
                .FirstOrDefault(c => c.Id == id);
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
