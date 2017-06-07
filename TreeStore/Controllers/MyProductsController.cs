using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TreeStore.Data;
using TreeStore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using TreeStore.Services;

namespace TreeStore.Controllers
{
    public class MyProductsController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public MyProductsController(IProductService _productService, ICategoryService _categoryService)
        {
            this.productService = _productService;
            this.categoryService = _categoryService;
        }

        // GET: MyProducts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = productService.GetProducts().AsQueryable().Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MyProducts/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await productService.GetProducts().AsQueryable()
                .Include(p => p.Category)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: MyProducts/Create
        public IActionResult Create()
        {
            var product = new Product();
            ViewData["CategoryId"] = new SelectList(categoryService.GetCategories(), "Id", "Name");
            return View(product);
        }

        // POST: MyProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,Price,DiscountPrice,ImagePath,IsActive,CompanyLink,CategoryId,Id,Name,CreateDate,UpdateDate,CreatedBy,UpdateBy")] Product product, IFormFile uploadFile)
        {
            if (product.DiscountPrice > product.Price)
            {
                ModelState.AddModelError("DiscountPrice", "İndirimli fiyat birim fiyattan yüksek olamaz");
            }

            if (uploadFile != null && ".jpg,.jpeg,.png".Contains(Path.GetExtension(uploadFile.FileName)) == false)
            {
                ModelState.AddModelError("ImageUpload", "Dosyanın uzantısı .jpg, .gif ya da .png olmalıdır.");
            }
            else if (ModelState.IsValid)
            {
                if (uploadFile != null)
                {
                    if (Path.GetExtension(uploadFile.FileName) == ".jpg"
                    || Path.GetExtension(uploadFile.FileName) == ".gif"
                    || Path.GetExtension(uploadFile.FileName) == ".png")
                    {
                        string category = DateTime.Now.Month + "-" + DateTime.Now.Year;
                        string FilePath = uploadFile + "\\uploads\\" + category + "\\";
                        string dosyaismi = Path.GetFileName(uploadFile.FileName);
                        var yuklemeYeri = Path.Combine(FilePath, dosyaismi);
                        product.ImagePath = "uploads/" + category + "/" + dosyaismi;
                        try
                        {
                            if (!Directory.Exists(FilePath))
                            {
                                Directory.CreateDirectory(FilePath);//Eðer klasör yoksa oluþtur    
                            }
                            using (var stream = new FileStream(yuklemeYeri, FileMode.Create))
                            {
                                await uploadFile.CopyToAsync(stream);
                            }
                            productService.CreateProduct(product);
                            productService.SaveProduct();
                            return RedirectToAction("Index");
                        }
                        catch (Exception exc) { ModelState.AddModelError("ProductImage", "Hata: " + exc.Message); }
                    }
                    else
                    {
                        ModelState.AddModelError("ProductImage", "Dosya uzantısı izin verilen uzantılardan olmalıdır.");
                    }
                }
                else { ModelState.AddModelError("FileExist", "Lütfen bir dosya seçiniz!"); }
            }
            ViewData["CategoryId"] = new SelectList(categoryService.GetCategories(), "Id", "Name", product.CategoryId);
        
            return View(product);
        }

        // GET: MyProducts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await productService.GetProducts().AsQueryable().SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
    
            ViewData["CategoryId"] = new SelectList(categoryService.GetCategories(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: MyProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Description,Price,DiscountPrice,ImagePath,IsActive,CompanyLink,CategoryId,Id,Name,CreateDate,UpdateDate,CreatedBy,UpdateBy")] Product product, IFormFile uploadFile)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            if (product.DiscountPrice > product.Price)
            {
                ModelState.AddModelError("DiscountPrice", "Lütfen kontrol ediniz.İndirimli fiyat daha düşük olmalıdır.!");
            }
            if (uploadFile != null && ".jpg,.jpeg,.png".Contains(Path.GetExtension(uploadFile.FileName)) == false)
            {
                ModelState.AddModelError("ImageUpload", "Dosyanın uzantısı .jpg, .gif ya da .png olmalıdır.");
            }

            if (ModelState.IsValid)
            {

                if (uploadFile != null)
                {


                    if (Path.GetExtension(uploadFile.FileName) == ".jpg"
                    || Path.GetExtension(uploadFile.FileName) == ".gif"
                    || Path.GetExtension(uploadFile.FileName) == ".png")
                    {
                        string category = DateTime.Now.Month + "-" + DateTime.Now.Year + "-ProductImages";
                        string FilePath = uploadFile + category + "\\";
                        string dosyaismi = Path.GetFileName(uploadFile.FileName);
                        var yuklemeYeri = Path.Combine(FilePath, dosyaismi);
                        product.ImagePath = "uploads/" + category + "/" + dosyaismi;
                        try
                        {
                            if (!Directory.Exists(FilePath))
                            {
                                Directory.CreateDirectory(FilePath);//Eðer klasör yoksa oluþtur    
                            }
                            using (var stream = new FileStream(yuklemeYeri, FileMode.Create))
                            {
                                uploadFile.CopyToAsync(stream);
                            }

                            productService.UpdateProduct(product);
                            productService.SaveProduct();
                            return RedirectToAction("Index");
                        }
                        catch (Exception exc) { ModelState.AddModelError("Image", "Hata: " + exc.Message); }
                    }
                    else
                    {
                        ModelState.AddModelError("ProductImage", "Dosya uzantısı izin verilen uzantılardan olmalıdır.");
                    }
                }
                else
                {

                    productService.UpdateProduct(product);
                    productService.SaveProduct();
                    return RedirectToAction("Index");
                }
            }
            ViewData["CategoryId"] = new SelectList(categoryService.GetCategories(), "Id", "Name", product.CategoryId);
          
            return View(product);
        
        }

        // GET: MyProducts/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await productService.GetProducts().AsQueryable()
                .Include(p => p.Category)
                .SingleOrDefaultAsync(m => m.Id == id);
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
