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
using MimeKit;
using MailKit.Net.Smtp;
using TreeStore.Services;
using Microsoft.AspNetCore.Authorization;

namespace TreeStore.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class MailSettingsController : Controller
    {
        private readonly IMailSettingService mailSettingService;
        private IHostingEnvironment env;
        public MailSettingsController(IMailSettingService _mailSettingService, IHostingEnvironment _env)
        {
            this.mailSettingService = _mailSettingService;
            this.env = _env;
        }

        // GET: Admin/MailSettings
        public IActionResult Index()
        {
            MailSetting cms;
            cms = mailSettingService.GetMailSettings().FirstOrDefault();
            return View(cms);
        }

        [HttpPost]
        public IActionResult Index(MailSetting mailSetting)
        {
            if (ModelState.IsValid)
            {
                MailSetting cms;
                if (mailSettingService.GetMailSettings().Any())
                {
                    cms = mailSettingService.GetMailSettings().FirstOrDefault();
                    mailSettingService.UpdateMailSetting(cms);
                    mailSettingService.SaveMailSetting();
                    ViewBag.Message = "Ayarlar baþarýyla güncellendi.";
                }
                else
                {
                    ViewBag.Message = "Ayarlar kaydedilemedi.";
                }
            }
            return View(mailSetting);
        }
    }
}