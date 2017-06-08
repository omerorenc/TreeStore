using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Models.Entities;

namespace TreeStore.Models.EntityBuilders
{
    public class SliderBuilder
    {
        public SliderBuilder(EntityTypeBuilder<Slider> entityBuilder)
        {
            entityBuilder.HasKey(s => s.Id);
            entityBuilder.Property(s => s.Name).HasMaxLength(200).IsRequired();
            
        }
    }
}
