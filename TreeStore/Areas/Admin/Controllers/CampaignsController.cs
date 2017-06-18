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
    [Authorize]
    [Area("Admin")]
    public class CampaignsController : Controller
    {
        private readonly ICampaignService campaignService;
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly ICategoryCampaignService categoryCampaignService;
        private readonly IProductCampaignService productCampaignService;

        public CampaignsController(ICampaignService campaignService, ICategoryService categoryService, IProductService productService
            ,ICategoryCampaignService categoryCampaignService
            ,IProductCampaignService productCampaignService)
        {
            this.campaignService = campaignService;
            this.productService = productService;
            this.categoryService = categoryService;
            this.categoryCampaignService = categoryCampaignService;
            this.productCampaignService = productCampaignService;
        }

        // GET: Admin/Campaigns
        public IActionResult Index()
        {

            var campaigns = campaignService.GetCampaigns();
            return View(campaigns);
        }

        // GET: Admin/Campaigns/Details/5
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categories = categoryService.GetCategories();
            var products = productService.GetProducts();
            var campaign =  campaignService.GetCampaigns().AsQueryable()
                .Include(c => c.CategoryCampaign)
                .Include(c => c.ProductCampaign)
                .SingleOrDefault(m => m.Id == id);
            ViewBag.Categories = categories;
            ViewBag.Products = products;
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }


        public IActionResult Create()
        {

            ViewData["Categories"] = new MultiSelectList(categoryService.GetCategories(), "Id", "Name");
            ViewData["Products"] = new MultiSelectList(productService.GetProducts(), "Id", "Name");
            return View();


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Campaign campaign)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    campaign.CreatedBy = User.Identity.Name;
                    campaign.UpdateBy = User.Identity.Name;
                    campaignService.CreateCampaign(campaign);
                    campaignService.SaveCampaign();

                    campaign.CategoryCampaign.Clear();
                    campaign.ProductCampaign.Clear();

                    if (campaign.CategoryIds != null)
                    {
                        foreach (var categoryId in campaign.CategoryIds)
                        {
                            campaign.CategoryCampaign.Add(new CategoryCampaign() { CampaignId = campaign.Id, CategoryId = categoryId });
                        }

                    }
                    if (campaign.ProductIds != null)
                    {
                        foreach (var productId in campaign.ProductIds)
                        {
                            campaign.ProductCampaign.Add(new ProductCampaign() { CampaignId = campaign.Id, ProductId = productId });
                        }
                    }
                    categoryService.SaveCategory();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Create");
                throw ex;
            }
            ViewData["Categories"] = new MultiSelectList(categoryService.GetCategories(), "Id", "Name",campaign.CategoryIds);
            ViewData["Products"] = new MultiSelectList(productService.GetProducts(), "Id", "Name",campaign.ProductIds);
            return View(campaign);
        }
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = campaignService.GetCampaignsWithRelated().SingleOrDefault(c => c.Id == id);
            if (campaign == null)
            {
                return NotFound();
            }
            ViewData["Categories"] = new MultiSelectList(categoryService.GetCategories(), "Id", "Name", campaign.CategoryCampaign.Select(c => c.CategoryId).ToList());
            ViewData["Products"] = new MultiSelectList(productService.GetProducts(), "Id", "Name", campaign.ProductCampaign.Select(c => c.ProductId).ToList());
            return View(campaign);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, Campaign campaign)
        {
            if (id != campaign.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    campaign.UpdateBy = User.Identity.Name;
                   
                    campaignService.UpdateCampaign(campaign);
                    campaignService.SaveCampaign();

                    
                    productCampaignService.DeleteRange(productCampaignService.GetProductCampaigns().Where(pc => pc.CampaignId == id).ToList());
                    categoryCampaignService.DeleteRange(categoryCampaignService.GetCategoryCampaigns().Where(cc => cc.CampaignId == id).ToList());
                    campaignService.SaveCampaign();
                    if (campaign.CategoryIds != null)
                    {
                        foreach (var categoryId in campaign.CategoryIds)
                        {
                            campaign.CategoryCampaign.Add(new CategoryCampaign() { CampaignId = campaign.Id, CategoryId = categoryId});
                        }

                    }
                    if (campaign.ProductIds != null)
                    {
                        foreach (var productId in campaign.ProductIds)
                        {
                            campaign.ProductCampaign.Add(new ProductCampaign() { CampaignId = campaign.Id, ProductId = productId });
                        }
                    }
                    
                    categoryService.SaveCategory();
                    
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
            ViewData["Categories"] = new MultiSelectList(categoryService.GetCategories(), "Id", "Name", campaign.CategoryIds);
            ViewData["Products"] = new MultiSelectList(productService.GetProducts(), "Id", "Name", campaign.ProductIds);
            return View(campaign);
        }

        // GET: Admin/Campaigns/Delete/5
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign =  campaignService.GetCampaignsWithRelated()
                .SingleOrDefault(m => m.Id == id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // POST: Admin/Campaigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            var campaign = campaignService.GetCampaignsWithRelated().FirstOrDefault(c => c.Id == id);
            campaign.ProductCampaign.Clear();
            campaign.CategoryCampaign.Clear();
            campaignService.DeleteCampaign(id);
            campaignService.SaveCampaign();
            return RedirectToAction("Index");
        }

        private bool CampaignExists(long id)
        {
            return campaignService.GetCampaigns().Any(e => e.Id == id);
        }
    }
}
