using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TreeStore.Data;
using TreeStore.Models.Entities;
using MimeKit;
using MailKit.Net.Smtp;
using TreeStore.Services;
using Microsoft.EntityFrameworkCore;
using TreeStore.Models;
using System.Collections.ObjectModel;

namespace TreeStore.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IMailSettingService mailSettingService;
        private readonly IContactService contactService;
        private readonly ISettingService settingService;
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly ICampaignService campaignService;
        private readonly ISubscriptionService subscriptionService;
        public HomeController( ISettingService settingService, IMailSettingService _mailSettingService, IContactService _contactService, ICategoryService categoryService, IProductService productService
            ,ICampaignService campaignService, ISubscriptionService _subscriptionService)
        {
         
            this.mailSettingService = _mailSettingService;
            this.contactService = _contactService;
            this.settingService = settingService;
            this.categoryService = categoryService;
            this.productService = productService;
            this.campaignService = campaignService;
            this.subscriptionService = _subscriptionService;
        }

        public IActionResult Index()
        {
            ViewBag.Facebook = settingService.GetSettings().FirstOrDefault().Facebook;
            ViewBag.Twitter = settingService.GetSettings().FirstOrDefault().Twitter;
            ViewBag.YouTube = settingService.GetSettings().FirstOrDefault().YouTube;
            ViewBag.RSS = settingService.GetSettings().FirstOrDefault().RSS;
            ViewBag.Pinterest = settingService.GetSettings().FirstOrDefault().Pinterest;
            ViewBag.Google = settingService.GetSettings().FirstOrDefault().Google;
            ViewBag.Instagram = settingService.GetSettings().FirstOrDefault().Instagram;
           
            var products = productService.GetProducts().Where(p => p.IsActive).OrderBy(p => p.CreateDate).Take(24);
            var campaigns = campaignService.GetCampaigns().Where(c => c.IsActive);
            var sliderProducts = productService.GetProducts().Where(p => p.SliderId != null);
            Collection<Campaign> viewCampaigns = new Collection<Campaign>();
            string[] names;
            foreach(var campaign in campaigns)
            {
                names = campaign.CreatedBy.Split('@');
                campaign.CreatedBy = names[0];
                
                viewCampaigns.Add(campaign);
            }
            CategoriesLayout();
            ViewBag.Products = products;
            ViewBag.Campaigns = viewCampaigns;
            ViewBag.SliderProducts = sliderProducts;
            return View();
        }

      
        [Route("Search")]
        public IActionResult Search(string query)
        {
            var product = from p in productService.GetProducts() select p;
            var campaign = from c in campaignService.GetCampaigns() select c;

            if (!String.IsNullOrEmpty(query))
            {
                product = product.Where(r =>
                   r.Name.ToString().ToLower().Contains(query) || r.Name.ToString().ToLower() == query.ToString().ToLower());
                campaign = campaign.Where(r =>
                    r.Name.ToString().ToLower().Contains(query) || r.Name.ToString().ToLower() == query.ToString().ToLower());
            }
            else
            {
                return RedirectToAction("index");
            }
            CategoriesLayout();
            ViewBag.product = productService.GetProducts().FirstOrDefault().CreatedBy;
            ViewBag.campaign = campaignService.GetCampaigns().FirstOrDefault().CreatedBy;
            ViewBag.ResultProduct = product.ToList();
            ViewBag.ResultCampaign = campaign.ToList();
            return View("Search");
        }
        [Route("urun")]
        public IActionResult Product(long? id)
        {
            var mainCategories = categoryService.GetCategories().Where(c => c.ParentCategoryId == null);
            var categories = categoryService.GetCategories().AsQueryable().Include(c => c.ChildCategories).Where(c => c.ParentCategoryId != null);

            var product = productService.GetProducts().FirstOrDefault(p => p.Id == id);
            ViewBag.Categories = categories;
            ViewBag.MainCategories = mainCategories;

            ViewBag.Product = product;
            CategoriesLayout();
            return View();
        }

        [Route("iletisim")]
        public IActionResult Contact()
        {
            CategoriesLayout();
            var contact = new Contact();
            ViewBag.Address = settingService.GetSettings().FirstOrDefault().Address;
            ViewBag.Phone = settingService.GetSettings().FirstOrDefault().Phone;
            ViewBag.Mail = settingService.GetSettings().FirstOrDefault().Mail;
            ViewBag.Fax = settingService.GetSettings().FirstOrDefault().Fax;
            return View(contact);
        }
        [HttpPost]
        [Route("iletisim")]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(Contact contact)
        {
            CategoriesLayout();
            if (ModelState.IsValid)
            {
                
                var mailSetting = mailSettingService.GetMailSettings().FirstOrDefault();
                Methods.SendMail(mailSetting, contact);
                contactService.CreateContact(contact);
                contactService.SaveContact();
                ViewBag.Message = "Mesajınız başarıyla gönderildi.";
                ViewBag.Address = settingService.GetSettings().FirstOrDefault().Address;
                ViewBag.Phone = settingService.GetSettings().FirstOrDefault().Phone;
                ViewBag.Mail = settingService.GetSettings().FirstOrDefault().Mail;
                ViewBag.Fax = settingService.GetSettings().FirstOrDefault().Fax;
                return View(contact);
            }
            ViewBag.Address = settingService.GetSettings().FirstOrDefault().Address;
            ViewBag.Phone = settingService.GetSettings().FirstOrDefault().Phone;
            ViewBag.Mail = settingService.GetSettings().FirstOrDefault().Mail;
            ViewBag.Fax = settingService.GetSettings().FirstOrDefault().Fax;
            
            return View(contact);
        }
        [Route("hakkinda")]
        public IActionResult About()
        {
            ViewBag.About = settingService.GetSettings().FirstOrDefault().About;
            CategoriesLayout();
            return View();
        }


        [Route("TermsOfUse")]
        public IActionResult TermsOfUse()
        {
            CategoriesLayout();
            ViewBag.TermsOfUse = settingService.GetSettings().FirstOrDefault().TermsOfUse;
            return View();
        }
        [Route("PrivacyPolicy")]
        public IActionResult PrivacyPolicy()
        {
            CategoriesLayout();
            ViewBag.PrivacyPolicy = settingService.GetSettings().FirstOrDefault().PrivacyPolicy;
            return View();
        }

        public IActionResult Error()
        {
            CategoriesLayout();
            return View();
        }

        public IActionResult Page404()
        {
            CategoriesLayout();
            return View();
        }

        [Route("Campaigns")]
        public IActionResult Campaigns()
        {
            List<Campaign> campaigns;
            campaigns = campaignService.GetCampaigns().ToList();
            CategoriesLayout();
            ViewBag.Campaigns = campaigns;
            return View();
        }

        [Route("Campaign")]
        public IActionResult Campaign(long? id)
        {
            var campaign = campaignService.GetCampaigns().FirstOrDefault(c => c.Id == id);
            ViewBag.Campaigns = campaign;
            CategoriesLayout();
            return View();
        }

        [Route("UserProducts")]
        public IActionResult UserProducts()
        {
            CategoriesLayout();
            return View();
        }

        [Route("Products")]
        public IActionResult Products(long? id)
        {
            List<Product> products;
            if (id != null) {
                products = productService.GetProducts().Where(p => p.IsActive && p.CategoryId == id).ToList();
                var category = categoryService.GetCategories().SingleOrDefault(c => c.Id == id);
                ViewBag.Category = category.Name;
             }
            else
            {
                products = productService.GetProducts().AsQueryable().Include(p => p.Category).Where(p => p.IsActive).ToList();
                ViewBag.Category = "ÜRÜNLER";
            }


            CategoriesLayout();
            ViewBag.Products = products;
         
            return View();
        }
       


        public IActionResult Subscribe(Subscription subscription)
        {


            var subs = subscriptionService.GetSubscriptions().FirstOrDefault(s => s.Email == subscription.Email);
            if (subs==null)

            {
                subscription.CreatedBy = User.Identity.Name ?? "username";
                subscription.UpdateBy = User.Identity.Name ?? "username";
                subscription.SubscriptionDate = DateTime.Now;
                subscription.ConfirmationCode = Guid.NewGuid().ToString();

                subscriptionService.CreateSubscription(subscription);
                subscriptionService.SaveSubscription();
            }
            CategoriesLayout();
            return RedirectToAction("Index");

        }

      public void CategoriesLayout()
        {
            var categories = categoryService.GetCategories().AsQueryable().Include(c => c.ChildCategories).Where(c => c.ParentCategoryId != null);
            var mainCategories = categoryService.GetCategories().Where(c => c.ParentCategoryId == null);
            ViewBag.MainCategories = mainCategories;
            ViewBag.Categories = categories;
        }

    }
}
