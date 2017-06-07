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

namespace TreeStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingsController : Controller
    {
        private readonly IHostingEnvironment env;
        private readonly ISettingService settingService;
        public SettingsController(IHostingEnvironment _env, ISettingService _settingService)
        {
            this.env = _env;
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