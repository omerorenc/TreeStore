using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TreeStore.Data;
using TreeStore.Models;
using TreeStore.Services;
using TreeStore.Data.Repositories;
using TreeStore.Data.Interface;
using TreeStore.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Globalization;

namespace TreeStore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddIdentity<ApplicationUser, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddMvc();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //Repositories

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ISettingRepository, SettingRepository>();
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IMailSettingRepository, MailSettingRepository>();
            services.AddTransient<IMediaRepository, MediaRepository>();
            services.AddTransient<ICampaignRepository, CampaignRepository>();
            services.AddTransient<ISliderRepository, SliderRepository>();
            services.AddTransient<ISubscriptionRepository, SubscriptionRepository>();
            services.AddTransient<IAdvertisementRepository, AdvertisementRepository>();

            //Services
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ISettingService, SettingService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<IMailSettingService, MailSettingService>();
            services.AddTransient<IMediaService, MediaService>();
            services.AddTransient<ICampaignService, CampaignService>();
            services.AddTransient<IProductCampaignService, ProductCampaignService>();
            services.AddTransient<ICategoryCampaignService, CategoryCampaignService>();
            services.AddTransient<ISliderService, SliderService>();
             services.AddTransient<ISubscriptionService, SubscriptionService>();
            services.AddTransient<IAdvertisementService, AdvertisementService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            var supportedCultures = new List<CultureInfo>
                {
                     new CultureInfo("en-US")
                      { NumberFormat = { CurrencySymbol = CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol + " " } }
                    };

        app.UseStaticFiles();

            app.UseIdentity();
            app.ApplicationServices.GetRequiredService<ApplicationDbContext>().Seed(app.ApplicationServices.GetRequiredService<UserManager<ApplicationUser>>(), app.ApplicationServices.GetRequiredService<RoleManager<Role>>());

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "areaRoute",
                    template: "{area:exists}/{controller=Dashboard}/{action=Index}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    "404-PageNotFound", // Route name
                    "{*url}",
                    new { controller = "Home", action = "Page404" } // Parameter defaults
                );
            });
        }
    }
}