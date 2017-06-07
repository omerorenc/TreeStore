﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Models;
namespace TreeStore.Models.EntityBuilders
{
    public class CategoryBuilder
    {
        public CategoryBuilder(EntityTypeBuilder<Category> entityBuilder)
        {
            entityBuilder.HasKey(c => c.Id);
            entityBuilder.Property(c => c.Name).HasMaxLength(200).IsRequired();
            entityBuilder.HasOne(c => c.ParentCategory)
                .WithMany(c => c.ChildCategories)
                .HasForeignKey(c => c.ParentCategoryId);
            entityBuilder.HasOne(c => c.Slider)
                .WithMany(s => s.Categories)
                .HasForeignKey(c => c.SliderId);
        }
    }
}
