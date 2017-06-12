using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Models.Entities;

namespace TreeStore.Models.EntityBuilders
{
    public class SubscriptionBuilder
    {
        public SubscriptionBuilder(EntityTypeBuilder<Subscription> entityBuilder)
        {
            entityBuilder.HasKey(p => p.Id);
            entityBuilder.Property(p => p.Email).HasMaxLength(200).IsRequired();
            entityBuilder.Property(p => p.FullName).HasMaxLength(200);
            entityBuilder.Property(p => p.ConfirmationCode).HasMaxLength(200);

        }
    }
}
