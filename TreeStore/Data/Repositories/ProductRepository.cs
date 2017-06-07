using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Data.Interface;
using TreeStore.Models;

namespace TreeStore.Data.Repositories
{
    public class ProductRepository : RepositoryBase<Product>,IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

    }

    public interface IProductRepository : IRepository<Product>
    {

    }
}
