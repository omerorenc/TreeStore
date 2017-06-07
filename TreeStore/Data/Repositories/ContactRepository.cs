using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Data.Interface;
using TreeStore.Models.Entities;

namespace TreeStore.Data.Repositories
{

    public class ContactRepository : RepositoryBase<Contact>, IContactRepository
    {
        public ContactRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }

    public interface IContactRepository : IRepository<Contact>
    {

    }
}
    

