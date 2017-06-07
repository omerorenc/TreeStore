using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Data.Interface;
using TreeStore.Models;

namespace TreeStore.Data.Repositories
{
    public class CampaignRepository : RepositoryBase<Campaign>,ICampaignRepository
    {
        public CampaignRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        
    }
    public interface ICampaignRepository : IRepository<Campaign>
    {

    }
}






