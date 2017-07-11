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

namespace TreeStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdvertisementsController : Controller
    {
        private readonly IAdvertisementService advertisementservice;

        public AdvertisementsController(IAdvertisementService advertisementservice)
        {
            this.advertisementservice = advertisementservice;
        }

        // GET: Admin/Advertisements
        public IActionResult Index()
        {
            var advertisements = advertisementservice.GetAdvertisements().ToList();
            ViewBag.Advertisements = advertisements;
            return View(advertisements);
        }

        // GET: Admin/Advertisements/Details/5
        public  IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement =  advertisementservice.GetAdvertisements().SingleOrDefault(a => a.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // GET: Admin/Advertisements/Create
        public IActionResult Create()
        {
         return View();
        }

        // POST: Admin/Advertisements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create([Bind("AdvertisementType,AdvertisementLocation,AdvertisementUrl,AdvertisementImage,AdvertisementDescription,IsActive,StartDate,FinishDate,Id,Name,CreateDate,UpdateDate,CreatedBy,UpdateBy")] Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                
                advertisement.CreatedBy = "username";
                advertisement.CreateDate = DateTime.Now;
                advertisement.UpdateBy = "username";
                advertisement.UpdateDate = DateTime.Now;
                advertisementservice.CreateAdvertisement(advertisement);
                advertisementservice.SaveAdvertisement();
                return RedirectToAction("Index");
            }
            return View(advertisement);
        }

        // GET: Admin/Advertisements/Edit/5
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = advertisementservice.GetAdvertisements().SingleOrDefault(a => a.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }
            return View(advertisement);
        }

        // POST: Admin/Advertisements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("AdvertisementType,AdvertisementLocation,AdvertisementUrl,AdvertisementImage,AdvertisementDescription,IsActive,StartDate,FinishDate,Id,Name,CreateDate,UpdateDate,CreatedBy,UpdateBy")] Advertisement advertisement)
        {
            if (id != advertisement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    advertisement.UpdateBy = "username";
                    advertisement.UpdateDate = DateTime.Now;
                    advertisementservice.UpdateAdvertisement(advertisement);
                    advertisementservice.SaveAdvertisement();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertisementExists(advertisement.Id))
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
            return View(advertisement);
        }

        // GET: Admin/Advertisements/Delete/5
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = advertisementservice.GetAdvertisements().SingleOrDefault(a=>a.Id==id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // POST: Admin/Advertisements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            advertisementservice.DeleteAdvertisement(id);
            advertisementservice.SaveAdvertisement();
            return RedirectToAction("Index");
        }

        private bool AdvertisementExists(long id)
        {

            return advertisementservice.GetAdvertisements().Any(e => e.Id == id);
        }
    }
}
