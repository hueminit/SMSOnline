using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Infrastructure
{
    public interface IRepository<T, K> where T : class // dữ liệu kiểu T với key là K với T là 1 class
    {
        Task<T> FindByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties);

        Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        Task<IQueryable<T>> FindAllAsync(params Expression<Func<T, object>>[] includeProperties);

        Task<IQueryable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        Task<bool> AddAsync(T entity);

        Task<bool> UpdateAsync(K id, T entity, params Expression<Func<T, object>>[] updatedProperties);

        Task<bool> RemoveAsync(T entity);

        Task<bool> RemoveByIdAsync(K id);

        Task<bool> RemoveMultipleAsync(List<T> entities);
    }
}