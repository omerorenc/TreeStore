using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Data;
using TreeStore.Models;

namespace TreeStore.Services
{
    public interface ICategoryCampaignService
    {

        IEnumerable<CategoryCampaign> GetCategoryCampaigns();
        void DeleteRange(IList<CategoryCampaign> list);

    }
    public class CategoryCampaignService : ICategoryCampaignService
    {
        private readonly ApplicationDbContext context;
        public CategoryCampaignService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void DeleteRange(IList<CategoryCampaign> list)
        {
            context.CategoryCampaigns.RemoveRange(list);
        }

        public IEnumerable<CategoryCampaign> GetCategoryCampaigns()
        {
            var CategoryCampaigns = context.CategoryCampaigns;
            return (CategoryCampaigns);
        }
    }
}
