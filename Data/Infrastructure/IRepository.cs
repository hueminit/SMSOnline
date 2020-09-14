using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Infrastructure
{
    public interface IRepository<T, K> where T : class // dữ liệu kiểu T với key là K với T là 1 class
    {
        T FindById(K id, params Expression<Func<T, object>>[] includeProperties);

        T FindSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        void Add(T entity);

        void Update(K id, T entity, params Expression<Func<T, object>>[] updatedProperties);

        void Remove(T entity);

        void RemoveById(K id);

        void RemoveMultiple(List<T> entities);
    }
}