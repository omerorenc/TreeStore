using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Data.Interface;
using TreeStore.Models;

namespace TreeStore.Data.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>,ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }

    public interface ICategoryRepository : IRepository<Category>
    {

    }
}


