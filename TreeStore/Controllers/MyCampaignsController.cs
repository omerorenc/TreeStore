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

namespace TreeStore.Controllers
{
    public class MyCampaignsController : Controller
    {
        private readonly ICampaignService CampaignService;

        public MyCampaignsController(ICampaignService _CampaignService)
        {
            this.CampaignService = _CampaignService;
        }

        // GET: MyCampaigns
        public IActionResult Index()
        {
            return View(CampaignService.GetCampaigns().ToList());
        }

        // GET: MyCampaigns/Details/5
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = CampaignService.GetCampaigns()
                .SingleOrDefault(m => m.Id == id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // GET: MyCampaigns/Create
        public IActionResult Create()
        {
            var myCampaign = new Campaign();
            return View(myCampaign);
        }

        // POST: MyCampaigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Description,Slogan,StartedDate,EndDate,ImagePath,IsActive,Id,Name,CreateDate,UpdateDate,CreatedBy,UpdateBy")] Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                CampaignService.CreateCampaign(campaign);
                CampaignService.SaveCampaign();
                return RedirectToAction("Index");
            }
            return View(campaign);
        }

        // GET: MyCampaigns/Edit/5
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = CampaignService.GetCampaigns().SingleOrDefault(m => m.Id == id);
            if (campaign == null)
            {
                return NotFound();
            }
            return View(campaign);
        }

        // POST: MyCampaigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Description,Slogan,StartedDate,EndDate,ImagePath,IsActive,Id,Name,CreateDate,UpdateDate,CreatedBy,UpdateBy")] Campaign campaign)
        {
            if (id != campaign.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    CampaignService.UpdateCampaign(campaign);
                    CampaignService.SaveCampaign();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampaignExists(campaign.Id))
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
            return View(campaign);
        }

        // GET: MyCampaigns/Delete/5
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = CampaignService.GetCampaigns()
                .SingleOrDefault(m => m.Id == id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // POST: MyCampaigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            var campaign = CampaignService.GetCampaigns().SingleOrDefault(m => m.Id == id);
            CampaignService.DeleteCampaign(id);
            CampaignService.DeleteCampaign(id);
            return RedirectToAction("Index");
        }

        private bool CampaignExists(long id)
        {
            return CampaignService.GetCampaigns().Any(e => e.Id == id);
        }
    }
}
