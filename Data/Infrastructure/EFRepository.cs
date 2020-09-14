using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Model.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Data.Infrastructure
{
    public class EFRepository<T, K> : IRepository<T, K>, IDisposable where T : DomainEntity<K>
    {
        private readonly AppDbContext _context;

        public EFRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                _context.Add(entity);
               return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                //todo
            }
            return await Task.FromResult(false);
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public async Task<IQueryable<T>> FindAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = _context.Set<T>();

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return await Task.FromResult(items.AsNoTracking());
        }

        public async Task<IQueryable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = _context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return await Task.FromResult(items.Where(predicate).AsNoTracking());
        }

        public async Task<T> FindByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties)
        {
            return await FindAllAsync(includeProperties).Result.SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return await FindAllAsync(includeProperties).Result.SingleOrDefaultAsync(predicate);
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            try
            {
                _context.Set<T>().Remove(entity);
               return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                //todo
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> RemoveByIdAsync(K id)
        {
            try
            {
                var entity = await FindByIdAsync(id);
                return await Task.FromResult(await RemoveAsync(entity));
            }
            catch (Exception e)
            {
                //todo
            }
            return await Task.FromResult(false);
            
        }

        public async Task<bool> RemoveMultipleAsync(List<T> entities)
        {
            try
            {
                _context.Set<T>().RemoveRange(entities);
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                //todo
            }
            return await Task.FromResult(false);
        }

        public virtual async Task<bool> UpdateAsync(K id, T entity, params Expression<Func<T, object>>[] updatedProperties)
        {   //chỉ update cái gì thay đổi
            var dbEntity = _context.Set<T>().AsNoTracking().Single(p => p.Id.Equals(id));
            var databaseEntry = _context.Entry(dbEntity);
            var inputEntry = _context.Entry(entity);
            if (updatedProperties.Any())
            {
                //update explicitly mentioned properties
                foreach (var property in updatedProperties)
                {
                    databaseEntry.Property(property).IsModified = true;
                    PropertyInfo prop = property.GetPropertyAccess();
                    databaseEntry.Property(property).IsModified = true;
                    databaseEntry.Property(property).CurrentValue = prop.GetValue(entity, null);
                }
            }
            else
            {
                //no items mentioned, so find out the updated entries
                IEnumerable<string> dateProperties = typeof(IDateTracking).GetPublicProperties().Select(x => x.Name);
                IEnumerable<string> domainProperties = typeof(DomainEntity<K>).GetPublicProperties().Select(x => x.Name);

                var allProperties = databaseEntry.Metadata.GetProperties()
                    .Where(x => !dateProperties.Contains(x.Name))
                    .Where(x => !domainProperties.Contains(x.Name));

                foreach (var property in allProperties)
                {
                    var proposedValue = inputEntry.Property(property.Name).CurrentValue;
                    var originalValue = databaseEntry.Property(property.Name).OriginalValue;

                    if ((proposedValue != null && !proposedValue.Equals(originalValue))
                        || (property.PropertyInfo.PropertyType.IsGenericType
                        && property.PropertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                    {
                        databaseEntry.Property(property.Name).IsModified = true;
                        databaseEntry.Property(property.Name).CurrentValue = proposedValue;
                    }
                }
            }

            try
            {
                _context.Set<T>().Update(dbEntity);
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                //todo
            }
            return await Task.FromResult(false);
        }

    }
}