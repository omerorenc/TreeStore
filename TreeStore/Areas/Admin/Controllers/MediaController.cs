using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TreeStore.Data;
using TreeStore.Models.Entities;
using TreeStore.Services;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace TreeStore.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class MediaController : Controller
    {
        private readonly IMediaService mediaService;
        private IHostingEnvironment env;
        protected string AssetsUrl;

        public MediaController(IMediaService _mediaService, IHostingEnvironment _env)
        {
            this.mediaService = _mediaService;
            this.env = _env;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var appSettings = (IOptions<AppSettings>)this.HttpContext.RequestServices.GetService(typeof(IOptions<AppSettings>));
            this.AssetsUrl = appSettings.Value.AssetsUrl;
            ViewBag.AssetsUrl = this.AssetsUrl;
            base.OnActionExecuting(filterContext);
        }

        // GET: Admin/Media
        public IActionResult Index()
        {
            return View(mediaService.GetMedias());
        }

        // GET: Admin/Media/Details/5
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media = mediaService.GetMedias().AsQueryable()
                .SingleOrDefault(m => m.Id == id);
            if (media == null)
            {
                return NotFound();
            }

            return View(media);
        }

        public JsonResult ModalCreate(string Title, string Description, IFormFile uploadFile)
        {
            //IFormFileCollection uploadedFiles = Request.Form.Files;
            //IFormFile uploadedFile = uploadedFiles[0];
            IFormFile file = Request.Form.Files[0];
            if (ModelState.IsValid)
            {
                if (uploadFile != null)
                {
                    Media media = new Media();
                    media.Title = Title;
                    media.Description = Description;
                    media.FileName = uploadFile.FileName;
                    media.Size = (uploadFile.Length / 1024);
                    media.CreatedBy = User.Identity.Name ?? "username";
                    media.CreateDate = DateTime.Now;
                    media.UpdateBy = User.Identity.Name ?? "username";
                    media.UpdateDate = DateTime.Now;

                    if (Path.GetExtension(uploadFile.FileName) == ".jpg" || Path.GetExtension(uploadFile.FileName) == ".jpeg" || Path.GetExtension(uploadFile.FileName) == ".png")
                    {
                        media.FileType = "Image";
                    }
                    else if (Path.GetExtension(uploadFile.FileName) == ".mp4" || Path.GetExtension(uploadFile.FileName) == ".gif")
                    {
                        media.FileType = "Video";
                    }
                    else
                    {
                        media.FileType = "Document";
                    }

                    if (Path.GetExtension(uploadFile.FileName) == ".doc"
                    || Path.GetExtension(uploadFile.FileName) == ".pdf"
                    || Path.GetExtension(uploadFile.FileName) == ".rtf"
                    || Path.GetExtension(uploadFile.FileName) == ".docx"
                    || Path.GetExtension(uploadFile.FileName) == ".jpg"
                    || Path.GetExtension(uploadFile.FileName) == ".gif"
                    || Path.GetExtension(uploadFile.FileName) == ".png"
                    || Path.GetExtension(uploadFile.FileName) == ".mp4"
                    || Path.GetExtension(uploadFile.FileName) == ".mp4"
                     )
                    {

                        string FilePath = env.WebRootPath + "\\uploads\\";
                        string dosyaismi = Path.GetFileName(uploadFile.FileName);
                        var yuklemeYeri = Path.Combine(FilePath, dosyaismi);
                        try
                        {
                            if (!Directory.Exists(FilePath))
                            {
                                Directory.CreateDirectory(FilePath);//Eðer klasör yoksa oluþtur    
                            }
                            using (var stream = new FileStream(yuklemeYeri, FileMode.Create))
                            {
                                uploadFile.CopyTo(stream);
                            }
                            mediaService.CreateMedia(media);
                            mediaService.SaveMedia();
                            return Json(new { result = AssetsUrl + media.FileUrl + media.FileName });
                        }
                        catch (Exception exc) { ModelState.AddModelError("FileName", "Hata: " + exc.Message); }
                    }
                    else
                    {
                        ModelState.AddModelError("FileName", "Dosya uzantısı izin verilen uzantılardan olmalıdır.");
                    }
                }
                else { ModelState.AddModelError("FileExist", "Lütfen bir dosya seçiniz!"); }
            }
            return Json(new { result = "false" });
        }

        public IEnumerable<Media> MediaGallery(string word, int? year, int? month, string category)
        {
            var mediagallery = mediaService.GetMedias().Where(w => w.CreateDate.Year == year && w.CreateDate.Month == month && w.FileType == category).ToList();

            if (!string.IsNullOrEmpty(word))
            {
                mediagallery = mediagallery.Where(w => w.Title.Contains(word) || w.Description.Contains(word) || w.FileName.Contains(word)).ToList();
            }
            return mediagallery;
        }


        public JsonResult ModalGallery(string word, int year, int month, string category)
        {
            var mediagallery = MediaGallery(word, year, month, category);
            return Json(new { result = mediagallery });
        }


        // GET: Admin/Media/Create
        public IActionResult Create(string element="")
        {
            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                ViewBag.Element = element;
                return View("ModalCreate");
            }
            return View();
        }

        // POST: Admin/Media/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,FileName,Description,Size,FileUrl,FileType,Id,Name,CreateDate,UpdateDate,CreatedBy,UpdateBy")] Media media, IFormFile uploadFile)
        {
            if (uploadFile != null && ".mp4,.gif,.jpg,.jpeg,.png,.pdf,.doc,.docx".Contains(Path.GetExtension(uploadFile.FileName)) == false)
            {
                ModelState.AddModelError("ImageUpload", "Dosyanýn uzantýsý .doc, .docx, .pdf, .rtf, .jpg, .gif, .mp4 ya da .png olmalýdýr.");
            }
            else if (ModelState.IsValid)
            {
                if (uploadFile != null)
                {
                    media.FileName = uploadFile.FileName;
                    media.Size = (uploadFile.Length / 1024);
                    media.CreatedBy = User.Identity.Name ?? "username";
                    media.CreateDate = DateTime.Now;
                    media.UpdateBy = User.Identity.Name ?? "username";
                    media.UpdateDate = DateTime.Now;
                   
                    if (Path.GetExtension(uploadFile.FileName) == ".jpg" || Path.GetExtension(uploadFile.FileName) == ".jpeg" || Path.GetExtension(uploadFile.FileName) == ".png")
                    {
                        media.FileType = "Image";
                    }
                    else if (Path.GetExtension(uploadFile.FileName) == ".mp4" || Path.GetExtension(uploadFile.FileName) == ".gif")
                    {
                        media.FileType = "Video";
                    }
                    else
                    {
                        media.FileType = "Document";
                    }

                    if (Path.GetExtension(uploadFile.FileName) == ".doc"
                    || Path.GetExtension(uploadFile.FileName) == ".pdf"
                    || Path.GetExtension(uploadFile.FileName) == ".rtf"
                    || Path.GetExtension(uploadFile.FileName) == ".docx"
                    || Path.GetExtension(uploadFile.FileName) == ".jpg"
                    || Path.GetExtension(uploadFile.FileName) == ".gif"
                    || Path.GetExtension(uploadFile.FileName) == ".png"
                    || Path.GetExtension(uploadFile.FileName) == ".mp4"
                    || Path.GetExtension(uploadFile.FileName) == ".mp4"
                     )
                    {
                        string category = DateTime.Now.Month + "-" + DateTime.Now.Year;
                        string FilePath = env.WebRootPath + "\\uploads\\" + category + "\\";
                        string dosyaismi = Path.GetFileName(uploadFile.FileName);
                        var yuklemeYeri = Path.Combine(FilePath, dosyaismi);
                   

                        try
                        {
                            if (!Directory.Exists(FilePath))
                            {
                                Directory.CreateDirectory(FilePath);//Eðer klasör yoksa oluþtur    
                            }
                            using (var stream = new FileStream(yuklemeYeri, FileMode.Create))
                            {
                                uploadFile.CopyTo(stream);
                            }


                            mediaService.CreateMedia(media);
                            mediaService.SaveMedia();
                            return RedirectToAction("Index");
                        }
                        catch (Exception exc) { ModelState.AddModelError("FileName", "Hata: " + exc.Message); }
                    }
                    else
                    {
                        ModelState.AddModelError("FileName", "Dosya uzantısı izin verilen uzantılardan olmalıdır.");
                    }
                }
                else { ModelState.AddModelError("FileExist", "Lütfen bir dosya seçiniz!"); }
            }
            return View(media);
        }

        // GET: Admin/Media/Edit/5
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media =  mediaService.GetMedias().AsQueryable().SingleOrDefault(m => m.Id == id);
            if (media == null)
            {
                return NotFound();
            }
            return View(media);
        }

        // POST: Admin/Media/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Title,FileName,Description,Size,FileUrl,FileType,Id,Name,CreateDate,UpdateDate,CreatedBy,UpdateBy")] Media media, IFormFile uploadFile)
        {
            if (ModelState.IsValid)
            {
                if (uploadFile != null)
                {
                    media.Id = id;
                    media.UpdateBy = User.Identity.Name ?? "username";
                    media.UpdateDate = DateTime.Now;
                    if (Path.GetExtension(uploadFile.FileName) == ".jpg" || Path.GetExtension(uploadFile.FileName) == ".jpeg" || Path.GetExtension(uploadFile.FileName) == ".png")
                    {
                        media.FileType = "Image";
                    }
                    else if (Path.GetExtension(uploadFile.FileName) == ".mp4" || Path.GetExtension(uploadFile.FileName) == ".gif")
                    {
                        media.FileType = "Video";
                    }
                    else
                    {
                        media.FileType = "Document";
                    }
                    if (Path.GetExtension(uploadFile.FileName) == ".doc"
                    || Path.GetExtension(uploadFile.FileName) == ".pdf"
                    || Path.GetExtension(uploadFile.FileName) == ".rtf"
                    || Path.GetExtension(uploadFile.FileName) == ".docx"
                    || Path.GetExtension(uploadFile.FileName) == ".jpg"
                    || Path.GetExtension(uploadFile.FileName) == ".gif"
                    || Path.GetExtension(uploadFile.FileName) == ".png"
                    || Path.GetExtension(uploadFile.FileName) == ".jpeg"
                    || Path.GetExtension(uploadFile.FileName) == ".mp4"
                     )
                    {
                        string category = DateTime.Now.Month + "-" + DateTime.Now.Year;
                        string FilePath = env.WebRootPath + "\\uploads\\"+ category + "\\";
                        string dosyaismi = Path.GetFileName(uploadFile.FileName);
                        var yuklemeYeri = Path.Combine(FilePath, dosyaismi);

                        try
                        {
                            if (!Directory.Exists(FilePath))
                            {
                                Directory.CreateDirectory(FilePath);//Eðer klasör yoksa oluþtur

                            }
                            using (var stream = new FileStream(yuklemeYeri, FileMode.Create))
                            {
                                uploadFile.CopyTo(stream);
                            }
                            media.FileName = uploadFile.FileName;
                            media.Size = (uploadFile.Length / 1024);
                            mediaService.UpdateMedia(media);
                            mediaService.SaveMedia();
                            return RedirectToAction("Index");
                        }
                        catch (Exception ex) { ModelState.AddModelError("FileName", "Hata: " + ex.Message); }
                    }
                    else
                    {
                        ModelState.AddModelError("FileName", "Dosya uzantýsý izin verilen uzantýlardan olmalýdýr.");
                    }
                }
                else
                {

                    media.UpdateBy = User.Identity.Name ?? "username";
                    media.UpdateDate = DateTime.Now;
                    mediaService.UpdateMedia(media);
                    mediaService.SaveMedia();
                    return RedirectToAction("Index");
                }
            }
            return View(media);
        }

        // GET: Admin/Media/Delete/5
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media =  mediaService.GetMedias().AsQueryable()
                .SingleOrDefault(m => m.Id == id);
            if (media == null)
            {
                return NotFound();
            }

            return View(media);
        }

        // POST: Admin/Media/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            var media =  mediaService.GetMedias().AsQueryable().SingleOrDefault(m => m.Id == id);
            mediaService.DeleteMedia(id);
            mediaService.SaveMedia();
            return RedirectToAction("Index");
        }

        private bool MediaExists(long id)
        {
            return mediaService.GetMedias().Any(e => e.Id == id);
        }
    }
}
