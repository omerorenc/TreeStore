using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Data;
using TreeStore.Models;

namespace TreeStore.Services
{
    public interface IProductCampaignService
    {

        IEnumerable<ProductCampaign> GetProductCampaigns();
        void DeleteRange(List<ProductCampaign> list);

    }
    public class ProductCampaignService : IProductCampaignService
    {
        private readonly ApplicationDbContext context;
        public ProductCampaignService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void DeleteRange(List<ProductCampaign> list)
        {
            context.ProductCampaigns.RemoveRange(list);
        }

        public IEnumerable<ProductCampaign> GetProductCampaigns()
        {
            var productCampaigns = context.ProductCampaigns;
            return (productCampaigns);
        }
    }
}
