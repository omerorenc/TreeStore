using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Models.Entities;

namespace TreeStore.Models.EntityBuilders
{
    public class ContactBuilder
    {
        public ContactBuilder(EntityTypeBuilder<Contact> entityBuilder)
        {
            entityBuilder.HasKey(c => c.Id);
            entityBuilder.Property(c => c.FullName).HasMaxLength(200).IsRequired();
            entityBuilder.Property(c => c.Email).HasMaxLength(200).IsRequired();
            entityBuilder.Property(c => c.Message).IsRequired();
            entityBuilder.Property(c => c.Phone).HasMaxLength(250);
        }
    }
}
