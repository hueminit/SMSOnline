using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        #region Properties

        private AppDbContext _dataContext;
        private readonly IDbSet<T> _dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected AppDbContext DbContext
        {
            get { return _dataContext ?? (_dataContext = DbFactory.Init()); }
        }

        #endregion Properties

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            _dbSet = DbContext.Set<T>();
        }

        #region

        public virtual async Task<T> Add(T entity)
        {
            return await Task.FromResult(_dbSet.Add(entity));
        }

        public virtual async Task Update(T entity)
        {
            _dbSet.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        public virtual async Task<T> Delete(T entity)
        {
            return await Task.FromResult(_dbSet.Remove(entity));
        }

        public virtual async Task<T> Delete(int id)
        {
            var entity = _dbSet.Find(id);
            return await Task.FromResult(_dbSet.Remove(entity));
        }

        public virtual async Task DeleteMulti(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                _dbSet.Remove(obj);
            await Task.CompletedTask;
        }

        public virtual async Task<T> GetSingleById(int id)
        {
            return await Task.FromResult(_dbSet.Find(id));
        }

        public virtual async Task<IQueryable<T>> GetMany(Expression<Func<T, bool>> where, string includes)
        {
            return await Task.FromResult(_dbSet.Where(where).AsNoTracking());
        }

        public virtual async Task<int> Count(Expression<Func<T, bool>> where)
        {
            return await Task.FromResult(_dbSet.Count(where));
        }

        public async Task<IQueryable<T>> GetAll(string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Any())
            {
                var query = _dataContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return await Task.FromResult(query.AsNoTracking());
            }

            return await Task.FromResult(_dataContext.Set<T>().AsNoTracking());
        }

        public async Task<T> GetSingleByConditionAsync(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            if (includes != null && includes.Any())
            {
                var query = _dataContext.Set<T>().Include(includes.First()).AsNoTracking();
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return await query.FirstOrDefaultAsync(expression);
            }
            return await _dataContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            if (includes != null && includes.Any())
            {
                var query = _dataContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.FirstOrDefault(expression);
            }
            return _dataContext.Set<T>().FirstOrDefault(expression);
        }

        public virtual async Task<IQueryable<T>> GetMultiAsync(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            if (includes != null && includes.Any())
            {
                var query = _dataContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return await Task.FromResult(query.Where<T>(predicate).AsNoTracking<T>());
            }

            return await Task.FromResult(_dataContext.Set<T>().Where<T>(predicate).AsNoTracking<T>());
        }

        public virtual IQueryable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            if (includes != null && includes.Any())
            {
                var query = _dataContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.Where<T>(predicate).AsNoTracking<T>();
            }

            return _dataContext.Set<T>().Where<T>(predicate).AsNoTracking<T>();
        }

        public virtual async Task<IQueryable<T>> GetMultiPaging(Expression<Func<T, bool>> predicate, int index = 0, int size = 20, string[] includes = null)
        {
            int skipCount = index * size;
            IQueryable<T> resetSet;

            if (includes != null && includes.Any())
            {
                var query = _dataContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                resetSet = predicate != null ? query.Where<T>(predicate).AsQueryable() : query.AsQueryable();
            }
            else
            {
                resetSet = predicate != null ? _dataContext.Set<T>().Where<T>(predicate).AsQueryable() : _dataContext.Set<T>().AsQueryable();
            }

            resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);
            //total = resetSet.Count();
            return await Task.FromResult(resetSet.AsNoTracking());
        }

        public async Task<bool> CheckContains(Expression<Func<T, bool>> predicate)
        {
            return await Task.FromResult(_dataContext.Set<T>().Count<T>(predicate) > 0);
        }

        #endregion
    }
}