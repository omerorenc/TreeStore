using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Data.Interface;
using TreeStore.Models.Entities;

namespace TreeStore.Data.Repositories
{
    public class MailSettingRepository : RepositoryBase<MailSetting>, IMailSettingRepository
    {
        public MailSettingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }

    public interface IMailSettingRepository : IRepository<MailSetting>
    {

    }
}
