using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Models.Entities;

namespace TreeStore.Models.EntityBuilders
{
    public class MailSettingBuilder
    {
        public MailSettingBuilder(EntityTypeBuilder<MailSetting> entityBuilder)
        {
            entityBuilder.HasKey(c => c.Id);
        }
    }
}
