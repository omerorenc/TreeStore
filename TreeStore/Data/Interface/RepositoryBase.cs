
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Threading.Tasks;
using TreeStore.Models;


namespace TreeStore.Data.Interface
{
    
    public abstract class RepositoryBase<T> where T:BaseEntity
    {
        private ApplicationDbContext context;
        private readonly DbSet<T> dataBaseSet;

        protected ApplicationDbContext DbContext
        {
            get { return context; }
        }

        protected RepositoryBase(ApplicationDbContext dbContext)
        {
            context = dbContext;
            dataBaseSet = dbContext.Set<T>();
        }

        
        public virtual void Add(T entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.UpdateDate=entity.CreateDate;
            dataBaseSet.Add(entity);
        }

        public virtual void Update(T entity)
        {

            entity.UpdateDate = DateTime.Now;
            dataBaseSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            dataBaseSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dataBaseSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
            {
                dataBaseSet.Remove(obj);
            }

        }

        public virtual T GetById(long id, params string[] navigations)
        {
            var set = dataBaseSet.AsQueryable();
            foreach(string nav in navigations)
            {
                set = set.Include(nav);
            }
            return set.FirstOrDefault(f => f.Id == id);
        }

        public virtual IEnumerable<T> GetAll(params string[] navigations)
        {
            var set = dataBaseSet.AsQueryable();
            foreach (string nav in navigations)
            {
                set = set.Include(nav);
            }

            return set.AsEnumerable();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where, params string[] navigations)
        {
            var set = dataBaseSet.AsQueryable();
            foreach (string nav in navigations)
            {
                set = set.Include(nav);
            }

            return set.Where(where).AsEnumerable();
        }

        public T Get(Expression<Func<T, bool>> where, params string[] navigations)
        {
            var set = dataBaseSet.AsQueryable();
            foreach (string nav in navigations)
            {
                set = set.Include(nav);
            }

            return set.Where(where).FirstOrDefault<T>();
        }
    }
}
