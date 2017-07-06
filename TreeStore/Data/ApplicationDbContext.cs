using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TreeStore.Models;
using TreeStore.Models.Entities;
using TreeStore.Models.EntityBuilders;

namespace TreeStore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCampaign> ProductCampaigns { get; set; }
        public DbSet<CategoryCampaign> CategoryCampaigns { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Role> ApplicationRole { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<MailSetting> MailSettings { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            new CategoryBuilder(builder.Entity<Category>());
            new CategoryCampaignBuilder(builder.Entity<CategoryCampaign>());
            new ProductCampaignBuilder(builder.Entity<ProductCampaign>());
            new ProductBuilder(builder.Entity<Product>());
            new CampaignBuilder(builder.Entity<Campaign>());
            new SliderBuilder(builder.Entity<Slider>());
            new SettingBuilder(builder.Entity<Setting>());
            new ContactBuilder(builder.Entity<Contact>());
            new MailSettingBuilder(builder.Entity<MailSetting>());
            new MediaBuilder(builder.Entity<Media>());
            new SubscriptionBuilder(builder.Entity<Subscription>());
            new AdvertisementBuilder(builder.Entity<Advertisement>());
        } 
        
       
    }
}
