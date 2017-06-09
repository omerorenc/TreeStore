using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TreeStore.Data;

namespace TreeStore.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170609120506_allMigration")]
    partial class allMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TreeStore.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address");

                    b.Property<string>("CompanyName");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("Fax");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Logo");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Phone");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("TreeStore.Models.Campaign", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("ImagePath");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<long?>("SliderId");

                    b.Property<string>("Slogan");

                    b.Property<DateTime>("StartedDate");

                    b.Property<string>("UpdateBy");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("SliderId");

                    b.ToTable("Campaigns");
                });

            modelBuilder.Entity("TreeStore.Models.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<long?>("ParentCategoryId");

                    b.Property<long?>("SliderId");

                    b.Property<string>("UpdateBy");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("ParentCategoryId");

                    b.HasIndex("SliderId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("TreeStore.Models.CategoryCampaign", b =>
                {
                    b.Property<long?>("CampaignId");

                    b.Property<long?>("CategoryId");

                    b.HasKey("CampaignId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("CategoryCampaigns");
                });

            modelBuilder.Entity("TreeStore.Models.Entities.Contact", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Message")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.Property<string>("Phone")
                        .HasMaxLength(250);

                    b.Property<string>("Reply");

                    b.Property<string>("UpdateBy");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("TreeStore.Models.Entities.MailSetting", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BodyContent")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("FromAddress")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("FromAddressPassword")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("FromAddressTitle")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Name");

                    b.Property<string>("Reply");

                    b.Property<int>("SmptPortNumber");

                    b.Property<string>("SmptServer")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("UpdateBy");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.ToTable("MailSettings");
                });

            modelBuilder.Entity("TreeStore.Models.Entities.Media", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Description");

                    b.Property<string>("FileName")
                        .HasMaxLength(200);

                    b.Property<string>("FileType")
                        .HasMaxLength(200);

                    b.Property<string>("FileUrl");

                    b.Property<string>("Name");

                    b.Property<decimal>("Size");

                    b.Property<string>("Title")
                        .HasMaxLength(200);

                    b.Property<string>("UpdateBy");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.ToTable("Media");
                });

            modelBuilder.Entity("TreeStore.Models.Entities.Setting", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("About");

                    b.Property<string>("Address");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Fax");

                    b.Property<string>("Mail");

                    b.Property<string>("MembershipAgreement");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<string>("PrivacyPolicy");

                    b.Property<string>("SeoDescription");

                    b.Property<string>("SeoKeywords");

                    b.Property<string>("SeoTitle");

                    b.Property<string>("TermsOfUse");

                    b.Property<string>("UpdateBy");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("WelcomeText");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("TreeStore.Models.Entities.Slider", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("UpdateBy");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.ToTable("Sliders");
                });

            modelBuilder.Entity("TreeStore.Models.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<long?>("CategoryId");

                    b.Property<string>("CompanyLink");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Description");

                    b.Property<decimal>("DiscountPrice");

                    b.Property<string>("ImagePath");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<decimal>("Price");

                    b.Property<long?>("SliderId");

                    b.Property<string>("UpdateBy");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SliderId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("TreeStore.Models.ProductCampaign", b =>
                {
                    b.Property<long>("CampaignId");

                    b.Property<long>("ProductId");

                    b.HasKey("CampaignId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductCampaigns");
                });

            modelBuilder.Entity("TreeStore.Models.Entities.Role", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole");


                    b.ToTable("Role");

                    b.HasDiscriminator().HasValue("Role");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TreeStore.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TreeStore.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TreeStore.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TreeStore.Models.Campaign", b =>
                {
                    b.HasOne("TreeStore.Models.ApplicationUser")
                        .WithMany("Campaigns")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("TreeStore.Models.Entities.Slider", "Slider")
                        .WithMany("Campaigns")
                        .HasForeignKey("SliderId");
                });

            modelBuilder.Entity("TreeStore.Models.Category", b =>
                {
                    b.HasOne("TreeStore.Models.ApplicationUser")
                        .WithMany("Categories")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("TreeStore.Models.Category", "ParentCategory")
                        .WithMany("ChildCategories")
                        .HasForeignKey("ParentCategoryId");

                    b.HasOne("TreeStore.Models.Entities.Slider", "Slider")
                        .WithMany("Categories")
                        .HasForeignKey("SliderId");
                });

            modelBuilder.Entity("TreeStore.Models.CategoryCampaign", b =>
                {
                    b.HasOne("TreeStore.Models.Campaign", "Campaign")
                        .WithMany("CategoryCampaign")
                        .HasForeignKey("CampaignId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TreeStore.Models.Category", "Category")
                        .WithMany("CategoryCampaign")
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("TreeStore.Models.Product", b =>
                {
                    b.HasOne("TreeStore.Models.ApplicationUser")
                        .WithMany("Products")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("TreeStore.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.HasOne("TreeStore.Models.Entities.Slider", "Slider")
                        .WithMany("Products")
                        .HasForeignKey("SliderId");
                });

            modelBuilder.Entity("TreeStore.Models.ProductCampaign", b =>
                {
                    b.HasOne("TreeStore.Models.Campaign", "Campaign")
                        .WithMany("ProductCampaign")
                        .HasForeignKey("CampaignId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TreeStore.Models.Product", "Product")
                        .WithMany("ProductCampaign")
                        .HasForeignKey("ProductId");
                });
        }
    }
}
