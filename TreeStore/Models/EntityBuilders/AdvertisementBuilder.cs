using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Models.Entities;

namespace TreeStore.Models.EntityBuilders
{
    public class AdvertisementBuilder
    {
        public AdvertisementBuilder(EntityTypeBuilder<Advertisement> entityBuilder)
        {
            entityBuilder.HasKey(c => c.Id);
                    entityBuilder.Property(c => c.Name).HasMaxLength(200).IsRequired();
                    entityBuilder.Property(c => c.AdvertisementImage).IsRequired();
                    entityBuilder.Property(c => c.AdvertisementLocation).IsRequired();
                    entityBuilder.Property(c => c.AdvertisementType).IsRequired();
                    entityBuilder.Property(c => c.AdvertisementUrl).IsRequired();
                    entityBuilder.Property(c => c.IsActive).IsRequired();
                    entityBuilder.Property(c => c.StartDate).IsRequired();
                    entityBuilder.Property(c => c.FinishDate).IsRequired();
                    entityBuilder.Property(c => c.StartDate).IsRequired();
                    entityBuilder.Property(c => c.FinishDate).IsRequired();
        }
    
    }
}
