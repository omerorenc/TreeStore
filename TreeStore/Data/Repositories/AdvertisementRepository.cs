using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Data.Interface;
using TreeStore.Models.Entities;

namespace TreeStore.Data.Repositories
{
    public class AdvertisementRepository:RepositoryBase<Advertisement>, IAdvertisementRepository
    {
        public AdvertisementRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

    }
    public interface IAdvertisementRepository : IRepository<Advertisement>
    {

    }
}
