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
            var mainCategories = categoryService.GetCategories().Where(c => c.ParentCategoryId == null);
            var categories = categoryService.GetCategories().Where(c => c.ParentCategoryId != null);
            var products = productService.GetProducts().Where(p => p.IsActive);
            var campaigns = campaignService.GetCampaigns().Where(c => c.IsActive);
            ViewBag.Categories = categories;
            ViewBag.MainCategories = mainCategories;
            ViewBag.Products = products;
            ViewBag.Campaigns = campaigns;
            return View();
        }

        [Route("hakkinda")]
        public IActionResult About()
        {
            ViewBag.About = settingService.GetSettings().FirstOrDefault().About;
            
            return View();
        }

        [Route("iletisim")]
        public IActionResult Contact()
        {
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


        [Route("TermsOfUse")]
        public IActionResult TermsOfUse()
        {
            ViewBag.TermsOfUse = settingService.GetSettings().FirstOrDefault().TermsOfUse;
            return View();
        }
        [Route("PrivacyPolicy")]
        public IActionResult PrivacyPolicy()
        {
            ViewBag.PrivacyPolicy = settingService.GetSettings().FirstOrDefault().PrivacyPolicy;
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Page404()
        {
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

            return RedirectToAction("Index");

        }

    }
}
