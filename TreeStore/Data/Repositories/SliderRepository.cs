using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Data.Interface;
using TreeStore.Models.Entities;

namespace TreeStore.Data.Repositories
{
    public class SliderRepository : RepositoryBase<Slider>, ISliderRepository
    {
        public SliderRepository(ApplicationDbContext dbContext)
                : base(dbContext) { }
       
    }
    public interface ISliderRepository : IRepository<Slider>
    {
        
    }
}