using Boilerplate.Application.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.Infrastructure.Persistence.Repositories;

public class Repository<TEntity>: IRepository<TEntity> where TEntity : class
{
    private readonly DbContext _context;

        protected RepositoryBase(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual TEntity? GetById(object id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual async Task<TEntity?> GetByIdAsync(object id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                _context.Set<TEntity>().Add(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                //ensure that the detailed error text is saved in the Log
                GetFullErrorTextAndRollbackEntityChanges();
            }
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual Task InsertAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return InsertInternalAsync(entity);
        }

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void BulkInsert(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            try
            {
                _context.Set<TEntity>().AddRange(entities);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                //ensure that the detailed error text is saved in the Log
                GetFullErrorTextAndRollbackEntityChanges();
            }
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entities">Entity</param>
        public virtual Task BulkInsertAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            return BulkInsertInternalAsync(entities);
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                _context.Set<TEntity>().Update(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                //ensure that the detailed error text is saved in the Log
                GetFullErrorTextAndRollbackEntityChanges();
            }
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return UpdateInternalAsync(entity);
        }

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void BulkUpdate(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            try
            {
                _context.Set<TEntity>().UpdateRange(entities);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                //ensure that the detailed error text is saved in the Log
                GetFullErrorTextAndRollbackEntityChanges();
            }
        }

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual async Task BulkUpdateAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new KeyNotFoundException(nameof(entities));
            }

            try
            {
                _context.Set<TEntity>().UpdateRange(entities);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                //ensure that the detailed error text is saved in the Log
                await GetFullErrorTextAndRollbackEntityChangesAsync();
            }
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                //ensure that the detailed error text is saved in the Log
                GetFullErrorTextAndRollbackEntityChanges();
            }
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public virtual Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return DeleteInternalAsync(entity);
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void BulkDelete(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            try
            {
                _context.Set<TEntity>().RemoveRange(entities);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                //ensure that the detailed error text is saved in the Log
                GetFullErrorTextAndRollbackEntityChanges();
            }
        }

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<TEntity> GetAllAsNoTracking()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public virtual IQueryable<TEntity> GetAllWithPagination(int pageNumber, int pageSize)
        {
            return _context.Set<TEntity>().Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking();
        }


        /// <summary>
        /// context save
        /// </summary>
        public virtual async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private async Task BulkInsertInternalAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                await _context.Set<TEntity>().AddRangeAsync(entities);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                //ensure that the detailed error text is saved in the Log
                await GetFullErrorTextAndRollbackEntityChangesAsync();
            }
        }
        
        private async Task InsertInternalAsync(TEntity entity)
        {
            try
            {
                await _context.Set<TEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                //ensure that the detailed error text is saved in the Log
                await GetFullErrorTextAndRollbackEntityChangesAsync();
            }
        }
        
        private async Task UpdateInternalAsync(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                //ensure that the detailed error text is saved in the Log
                await GetFullErrorTextAndRollbackEntityChangesAsync();
            }
        }
        
        private async Task DeleteInternalAsync(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                //ensure that the detailed error text is saved in the Log
                await GetFullErrorTextAndRollbackEntityChangesAsync();
            }
        }

        /// <summary>
        /// Rollback of entity changes and return full error message
        /// </summary>
        /// <returns>Error message</returns>
        private void GetFullErrorTextAndRollbackEntityChanges()
        {
            //rollback entity changes
            if (_context is { } dbContext)
            {
                var entries = dbContext.ChangeTracker.Entries()
                    .Where(e => e.State is EntityState.Added or EntityState.Modified).ToList();

                entries.ForEach(entry => entry.State = EntityState.Unchanged);
            }

            _context.SaveChanges();
        }

        /// <summary>
        /// Rollback of entity changes and return full error message
        /// </summary>
        /// <returns>Error message</returns>
        private async Task GetFullErrorTextAndRollbackEntityChangesAsync()
        {
            //rollback entity changes
            if (_context is { } dbContext)
            {
                var entries = dbContext.ChangeTracker.Entries()
                    .Where(e => e.State is EntityState.Added or EntityState.Modified).ToList();

                entries.ForEach(entry => entry.State = EntityState.Unchanged);
            }

            await _context.SaveChangesAsync();
        }
}
