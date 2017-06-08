using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Models.Entities;

namespace TreeStore.Models.EntityBuilders
{
    public class MediaBuilder
    {
        public MediaBuilder(EntityTypeBuilder<Media> entityBuilder)
        {
            entityBuilder.HasKey(c => c.Id);
            entityBuilder.Property(c => c.Title).HasMaxLength(200);
            entityBuilder.Property(c => c.FileType).HasMaxLength(200);
            entityBuilder.Property(c => c.FileName).HasMaxLength(200);
        }
    }
}
