using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        Task<T> Add(T entity);

        Task Update(T entity);

        Task<T> Delete(T entity);

        Task<T> Delete(int id);

        Task DeleteMulti(Expression<Func<T, bool>> where);

        Task<T> GetSingleById(int id);

        Task<T> GetSingleByConditionAsync(Expression<Func<T, bool>> expression, string[] includes = null);

        T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null);

        Task<IQueryable<T>> GetAll(string[] includes = null);

        Task<IQueryable<T>> GetMultiAsync(Expression<Func<T, bool>> predicate, string[] includes = null);

        IQueryable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null);

        Task<IQueryable<T>> GetMultiPaging(Expression<Func<T, bool>> filter, int index = 0, int size = 50, string[] includes = null);

        Task<int> Count(Expression<Func<T, bool>> where);

        Task<bool> CheckContains(Expression<Func<T, bool>> predicate);
    }
}