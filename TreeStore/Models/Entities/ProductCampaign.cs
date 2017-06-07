using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreeStore.Models
{
    public class ProductCampaign
    {
        public long ProductId { get; set; }
        public long CampaignId { get; set; }
        public Product Product { get; set; }
        public Campaign Campaign { get; set; }
    }
}
