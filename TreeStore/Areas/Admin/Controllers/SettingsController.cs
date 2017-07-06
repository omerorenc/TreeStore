using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TreeStore.Data;
using TreeStore.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using TreeStore.Services;
using Microsoft.AspNetCore.Authorization;

namespace TreeStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class SettingsController : Controller
    {
        private readonly ISettingService settingService;
        public SettingsController(ISettingService _settingService)
        {
            this.settingService = _settingService;
        }
        public IActionResult Index()
        {
            var setting = settingService.GetSettings().FirstOrDefault();
            return View(setting);
        }
        [HttpPost]
        public IActionResult Index(Setting setting, long id)
        {
            if (ModelState.IsValid)
            {
                if (setting != null)
                {
                    settingService.UpdateSetting(setting);
                    settingService.SaveSetting();
                    ViewBag.Message = "Ayarlar baþarýyla güncellendi.";
                    ViewBag.Title = setting.SeoTitle;
                    ViewBag.MembershipAgreement = setting.MembershipAgreement;
                    ViewBag.SeoDescription = setting.SeoDescription;
                    ViewBag.SeoKeywords = setting.SeoKeywords;
                    ViewBag.Address = setting.Address;
                    ViewBag.Phone = setting.Phone;
                    ViewBag.Fax = setting.Fax;
                    ViewBag.Mail = setting.Mail;
                    ViewBag.About = setting.About;
                    ViewBag.PrivacyPolicy = setting.PrivacyPolicy;
                    ViewBag.TermsOfUse = setting.TermsOfUse;
                    ViewBag.Twitter = setting.Twitter;
                    ViewBag.Facebook = setting.Facebook;
                    ViewBag.Google = setting.Google;
                    ViewBag.Instagram = setting.Instagram;
                    ViewBag.Pinterest = setting.Pinterest;
                    ViewBag.RSS = setting.RSS;
                    ViewBag.YouTube = setting.YouTube;


                }
                else
                {
                    ViewBag.Message = "Ayarlar kaydedilemedi.";
                }
            }
            return View(setting);
        }
    }
}