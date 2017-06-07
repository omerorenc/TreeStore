using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
        }
    }
}
