using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreeStore.Models
{
    public class CategoryCampaign
    {
        public long? CategoryId { get; set; }
        public long? CampaignId { get; set; }
        public Category Category { get; set; }
        public Campaign Campaign { get; set; }
    }
}
