using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestApp.API.Data.Base
{
    public interface IBaseRepository<T>
    {
         IEnumerable<T> FindAll();   
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();

        #region Async
         Task<IEnumerable<T>> FindAllAsync();
         Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveAsync();
        #endregion
    }
}