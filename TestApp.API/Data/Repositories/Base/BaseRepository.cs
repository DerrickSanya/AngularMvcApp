namespace TestApp.API.Data.Repositories.Base
{
    using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using TestApp.API.Data.Base;
using TestApp.API.Data.Context;

    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected TestAppDataContext dbContext { get; set; }
         public BaseRepository(TestAppDataContext databaseContext)
        {
            this.dbContext = databaseContext;
        }

        public IEnumerable<T> FindAll()
        {
            return this.dbContext.Set<T>();
        }
 
        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.dbContext.Set<T>().Where(expression);
        }
 
        public void Create(T entity)
        {
            this.dbContext.Set<T>().Add(entity);
        }
 
        public void Update(T entity)
        {
            this.dbContext.Set<T>().Update(entity);
        }
 
        public void Delete(T entity)
        {
            this.dbContext.Set<T>().Remove(entity);
        }
 
        public void Save()
        {
            this.dbContext.SaveChanges();
        }

         #region Async
        public async Task<IEnumerable<T>> FindAllAsync()
        {
             return await this.dbContext.Set<T>().ToListAsync();
        }
        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await this.dbContext.Set<T>().Where(expression).ToListAsync();
        }
       public async Task CreateAsync(T entity)
       {          
          using (var ctx = this.dbContext)
            {
                ctx.Set<T>().Add(entity);
                await ctx.SaveChangesAsync();
            }
       }
        public async Task UpdateAsync(T entity)
        {
          using (var ctx = this.dbContext)
            {
                ctx.Set<T>().Update(entity);
                await ctx.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(T entity)
        {
             using (var ctx = this.dbContext)
            {
                ctx.Set<T>().Remove(entity);
                await ctx.SaveChangesAsync();
            }
        }
        public async Task SaveAsync()
        {
           await this.dbContext.SaveChangesAsync();
        }
        #endregion
    }
}