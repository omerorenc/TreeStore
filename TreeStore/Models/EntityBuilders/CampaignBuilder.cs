using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreeStore.Models.EntityBuilders
{
    public class CampaignBuilder
    {
        public CampaignBuilder(EntityTypeBuilder<Campaign> entityBuilder)
        {
            entityBuilder.HasKey(c => c.Id);
            entityBuilder.Property(c => c.Name).HasMaxLength(200).IsRequired();
            entityBuilder.Property(c => c.StartedDate).IsRequired();
            entityBuilder.Property(c => c.EndDate).IsRequired();

        }
    }
}
