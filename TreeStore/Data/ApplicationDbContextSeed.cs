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
            AddCampaign(context);
            AddProducts(context);
            AddSlider(context);
        }

        private static void AddSlider(ApplicationDbContext context)
        {
            context.AddRange(
                new Slider { Name = "Slider", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
                new Slider { Name = "Slider2", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
                );

            context.SaveChanges();
        }

        private static void AddCategories(ApplicationDbContext context)
        {
            context.AddRange(
                new Category { Name = "Beyaz Eşya", CreateDate = DateTime.Now, CreatedBy = user.UserName },
                new Category { Name = "Market", CreateDate = DateTime.Now, CreatedBy = user.UserName },
                new Category { Name = "Elektronik", CreateDate = DateTime.Now, CreatedBy = user.UserName },
                new Category { Name = "Giyim", CreateDate = DateTime.Now, CreatedBy = user.UserName },
                new Category { Name = "Erkek", ParentCategoryId = 4, CreateDate = DateTime.Now, CreatedBy = user.UserName },
                new Category { Name = "Kadın", ParentCategoryId = 4, CreateDate = DateTime.Now, CreatedBy = user.UserName },
                new Category { Name = "Çocuk", ParentCategoryId = 4, CreateDate = DateTime.Now, CreatedBy = user.UserName },
                new Category { Name = "Buzdolabı", ParentCategoryId = 1, CreateDate = DateTime.Now, CreatedBy = user.UserName },
                new Category { Name = "Bilgisayar", ParentCategoryId = 3, CreateDate = DateTime.Now, CreatedBy = user.UserName },
                new Category { Name = "İçecek", ParentCategoryId = 2, CreateDate = DateTime.Now, CreatedBy = user.UserName }
                );
            context.SaveChanges();
        }

        private static void AddCampaign(ApplicationDbContext context)
        {
            context.AddRange(
                new Campaign { Name = "Kampanya", Slogan = "Büyük Kampanya", Description = "Açıklama", StartedDate = DateTime.Now, EndDate = DateTime.Now, IsActive = true, ImagePath = "kampanya.png", CreateDate = DateTime.Now, CreatedBy = user.UserName },
                 new Campaign { Name = "Kampanya2", Slogan = "Büyük Kampanya", Description = "Açıklama", StartedDate = DateTime.Now, EndDate = DateTime.Now, IsActive = true, ImagePath = "kampanya1.png", CreateDate = DateTime.Now, CreatedBy = user.UserName },
                  new Campaign { Name = "Kampanya3", Slogan = "Büyük Kampanya", Description = "Açıklama", StartedDate = DateTime.Now, EndDate = DateTime.Now, IsActive = true, ImagePath = "kampanya2.png", CreateDate = DateTime.Now, CreatedBy = user.UserName },
                   new Campaign { Name = "Kampanya4", Slogan = "Büyük Kampanya", Description = "Açıklama", StartedDate = DateTime.Now, EndDate = DateTime.Now, IsActive = true, ImagePath = "kampanya3.png", CreateDate = DateTime.Now, CreatedBy = user.UserName }
                );
            context.SaveChanges();
        }
        public static void AddProducts(ApplicationDbContext context)
        {
            context.AddRange(
                new Product
                {
                    Name = "Asus X550V",
                    CreatedBy = user.UserName,
                    CreateDate = DateTime.Now,
                    UpdateBy = user.UserName,
                    UpdateDate = DateTime.Now,
                    CategoryId = 9,
                    Description = "Asus X550V",
                    ImagePath = "asus.jpg",
                    DiscountPrice = 3000,
                    Price = 3200,
                    IsActive = true,
                    SliderId =1
                },
                   new Product
                   {
                       Name = "Arcelik Buzdolabı",
                       CreatedBy = user.UserName,
                       CreateDate = DateTime.Now,
                       UpdateBy = user.UserName,
                       UpdateDate = DateTime.Now,
                       CategoryId = 8,
                       Description = "Buzdolabı",
                       ImagePath = "arcelik.jpg",
                       DiscountPrice = 1500,
                       Price = 1700,
                       IsActive = true,
                       SliderId = 1
                   },
                      new Product
                      {
                          Name = "Mavi Tişört",
                          CreatedBy = user.UserName,
                          CreateDate = DateTime.Now,
                          UpdateBy = user.UserName,
                          UpdateDate = DateTime.Now,
                          CategoryId = 5,
                          Description = "Tişört",
                          ImagePath = "mavi.jpg",
                          DiscountPrice = 70,
                          Price = 99,
                          IsActive = true,
                          SliderId = 1
                      },
                         new Product
                         {
                             Name = "Bluz",
                             CreatedBy = user.UserName,
                             CreateDate = DateTime.Now,
                             UpdateBy = user.UserName,
                             UpdateDate = DateTime.Now,
                             CategoryId = 6,
                             Description = "Bluz",
                             ImagePath = "bluz.jpg",
                             DiscountPrice = 50,
                             Price = 100,
                             IsActive = true,
                             SliderId = 1
                         },
                            new Product
                            {
                                Name = "Çocuk Ayakkabısı",
                                CreatedBy = user.UserName,
                                CreateDate = DateTime.Now,
                                UpdateBy = user.UserName,
                                UpdateDate = DateTime.Now,
                                CategoryId = 7,
                                Description = "Çocuk Ayakkabısı",
                                ImagePath = "cocukayakkabi.jpg",
                                DiscountPrice = 99,
                                Price = 140,
                                IsActive = true,
                                SliderId = 1
                            },
                            new Product
                            {
                                Name = "Kadın Parfüm",
                                CreatedBy = user.UserName,
                                CreateDate = DateTime.Now,
                                UpdateBy = user.UserName,
                                UpdateDate = DateTime.Now,
                                CategoryId = 6,
                                Description = "Kadın Parfüm",
                                ImagePath = "kadinparfum.jpg",
                                DiscountPrice = 224,
                                Price = 250,
                                IsActive = true,
                                SliderId = 1
                            });
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
            s.Facebook = "https://www.facebook.com";
            s.Google = "https://plus.google.com";
            s.Twitter = "https://twitter.com";
            s.YouTube = "https://www.youtube.com";
            s.RSS = "https://www.rss.com";
            s.Pinterest = "https://tr.pinterest.com";
            s.Instagram = "https://www.instagram.com";
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
