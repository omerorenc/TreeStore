using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Models;
using TreeStore.Models.Entities;

namespace TreeStore.Data
{
    public static class ApplicationDbContextSeed
    {
        public static void Seed(this ApplicationDbContext context, UserManager<ApplicationUser> _userManager, RoleManager<Role> _roleManager)
        {
            context.Database.Migrate();
            if (context.MailSettings.Any())
            {
                return;  
            }
            AddMailSettings(context);
            AddSettings(context);
            AddUsers(context, _userManager);
            AddRoles(_roleManager);
            AddRoleToUser(_userManager);
            AddCategories(context);
        }

        private static void AddCategories(ApplicationDbContext context)
        {
            context.AddRange(
                new Category { Name = "Beyaz Eşya" },
                new Category { Name = "Market" },
                new Category { Name = "Elektronik" },
                new Category { Name = "Giyim" },
                new Category { Name = "Erkek", ParentCategoryId = 4 },
                new Category { Name = "Kadın", ParentCategoryId = 4 },
                new Category { Name = "Çocuk", ParentCategoryId = 4 }
                );
            context.SaveChanges();
        }

        public static void AddMailSettings(ApplicationDbContext context)
        {
            var cms = new MailSetting();
            cms.FromAddress = "treestorebem@gmail.com";
            cms.FromAddressPassword = "Treestore.9";
            cms.FromAddressTitle = "Tree Store";
            cms.Subject = "İletişim";
            cms.BodyContent = "Mesajınız Bize İletilmiştir. İlginiz İçin Teşekkür Ederiz";
            cms.SmptServer = "smtp.gmail.com";
            cms.SmptPortNumber = 587;
            context.MailSettings.Add(cms);
            context.SaveChanges();
        }

        public static void AddSettings(ApplicationDbContext context)
        {
            var s = new Setting();
            s.SeoTitle = "İndirimli Ürünler Sitesi";
            s.SeoDescription = s.SeoTitle;
            s.SeoKeywords = "indirim, discount, alışveriş";
            s.Address = "Kadıköy";
            s.Phone = "02122121212";
            s.Fax = "02122122121";
            s.Mail = "ornek@mail.com";
            context.Settings.Add(s);
            context.SaveChanges();
        }
        static ApplicationUser user;
        private static void AddUsers(ApplicationDbContext context, UserManager<ApplicationUser> _userManager)
        {
            user = new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = "arzugedik312@gmail.com", Email = "arzugedik312@gmail.com", EmailConfirmed = true, NormalizedEmail = "ARZUGEDIK312@GMAIL.COM", NormalizedUserName = "ARZUGEDIK312@GMAIL.COM" };
            var task1 = Task.Run(() => _userManager.CreateAsync(user, "Treestore123+"));
            task1.Wait();
        }

        private static void AddRoles(RoleManager<Role> _roleManager)
        {
            string[] roles = { "Admin", "Firma Sahibi" ,"Onaylanmamis Uye"};
            string[] stamp = { "1", "2" , "3" };

            for (int i = 0; i < roles.Count(); i++)
            {
                var role = new Role { Id = Guid.NewGuid().ToString(), Name = roles[i], NormalizedName = roles[i], ConcurrencyStamp = stamp[i] };
                var task1 = Task.Run(() => _roleManager.CreateAsync(role));
                task1.Wait();
            }
        }
        private static void AddRoleToUser(UserManager<ApplicationUser> _userManager)
        {
            var task1 = Task.Run(() => _userManager.AddToRoleAsync(user, "Admin"));
            task1.Wait();
        }
    }
}
