using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Data.Interface;
using TreeStore.Models.Entities;

namespace TreeStore.Data.Repositories
{
    public class MediaRepository : RepositoryBase<Media>, IMediaRepository
    {
        public MediaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

    }
    public interface IMediaRepository : IRepository<Media>
    {

    }
}
