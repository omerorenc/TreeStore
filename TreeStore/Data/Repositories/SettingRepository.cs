using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Data.Interface;
using TreeStore.Models.Entities;

namespace TreeStore.Data.Repositories
{
   
    public class SettingRepository : RepositoryBase<Setting>, ISettingRepository
    {
        public SettingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }

    public interface ISettingRepository : IRepository<Setting>
    {

    }
}
