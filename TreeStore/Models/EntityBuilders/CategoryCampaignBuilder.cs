using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreeStore.Models.EntityBuilders
{
    public class CategoryCampaignBuilder
    {
        public CategoryCampaignBuilder(EntityTypeBuilder<CategoryCampaign> entityBuilder)
        {

            entityBuilder.HasKey(cc => new { cc.CampaignId, cc.CategoryId });
            entityBuilder.HasOne(cc => cc.Category)
                .WithMany(c => c.CategoryCampaign)
                .HasForeignKey(cc => cc.CategoryId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasOne(cc => cc.Campaign)
                .WithMany(cp => cp.CategoryCampaign)
                .HasForeignKey(cc => cc.CampaignId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
