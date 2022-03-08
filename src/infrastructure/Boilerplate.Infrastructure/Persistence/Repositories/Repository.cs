using Boilerplate.Application.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.Infrastructure.Persistence.Repositories;

public class Repository<TEntity>: IRepository<TEntity> where TEntity : class
{
     #region Fields

    private readonly DbContext _context;

    #endregion

    #region Ctor

    protected Repository(DbContext context)
    {
        _context = context;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Get entity by identifier
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <returns>Entity</returns>
    public virtual TEntity GetById(object id)
    {
        return _context.Set<TEntity>().Find(id);
    }

    /// <summary>
    /// Get entity by identifier
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <returns>Entity</returns>
    public virtual async Task<TEntity> GetByIdAsync(object id)
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
        catch (DbUpdateException exception)
        {
            //ensure that the detailed error text is saved in the Log
            throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
        }
    }

    /// <summary>
    /// Insert entity
    /// </summary>
    /// <param name="entity">Entity</param>
    public virtual async Task InsertAsync(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        try
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException exception)
        {
            //ensure that the detailed error text is saved in the Log
            throw new Exception(await GetFullErrorTextAndRollbackEntityChangesAsync(exception), exception);
        }
    }

    /// <summary>
    /// Insert entities
    /// </summary>
    /// <param name="entities">Entities</param>
    public virtual void Insert(IEnumerable<TEntity> entities)
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
        catch (DbUpdateException exception)
        {
            //ensure that the detailed error text is saved in the Log
            throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
        }
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
        catch (DbUpdateException exception)
        {
            //ensure that the detailed error text is saved in the Log
            throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
        }
    }

    /// <summary>
    /// Update entity
    /// </summary>
    /// <param name="entity">Entity</param>
    public virtual async Task UpdateAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        try
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException exception)
        {
            //ensure that the detailed error text is saved in the Log
            throw new Exception(await GetFullErrorTextAndRollbackEntityChangesAsync(exception), exception);
        }
    }

    /// <summary>
    /// Update entities
    /// </summary>
    /// <param name="entities">Entities</param>
    public virtual void Update(IEnumerable<TEntity> entities)
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
        catch (DbUpdateException exception)
        {
            //ensure that the detailed error text is saved in the Log
            throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
        }
    }

    /// <summary>
    /// Delete entity
    /// </summary>
    /// <param name="entity">Entity</param>
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
        catch (DbUpdateException exception)
        {
            //ensure that the detailed error text is saved in the Log
            throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
        }
    }

    /// <summary>
    /// Delete entity
    /// </summary>
    /// <param name="entity">Entity</param>
    public virtual async Task DeleteAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        try
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException exception)
        {
            //ensure that the detailed error text is saved in the Log
            throw new Exception(await GetFullErrorTextAndRollbackEntityChangesAsync(exception), exception);
        }
    }

    /// <summary>
    /// Delete entities
    /// </summary>
    /// <param name="entities">Entities</param>
    public virtual void Delete(IEnumerable<TEntity> entities)
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
        catch (DbUpdateException exception)
        {
            //ensure that the detailed error text is saved in the Log
            throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
        }
    }

    #endregion

    #region Properties

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

    public virtual IQueryable<TEntity> GetAll(int pageNumber, int pageSize)
    {
        return _context.Set<TEntity>().Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking();
    }

    #endregion

    #region Utilities

    /// <summary>
    /// Rollback of entity changes and return full error message
    /// </summary>
    /// <param name="exception">Exception</param>
    /// <returns>Error message</returns>
    private string GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
    {
        //rollback entity changes
        if (_context is DbContext dbContext)
        {
            var entries = dbContext.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

            entries.ForEach(entry => entry.State = EntityState.Unchanged);
        }

        _context.SaveChanges();
        return exception.ToString();
    }

    /// <summary>
    /// Rollback of entity changes and return full error message
    /// </summary>
    /// <param name="exception">Exception</param>
    /// <returns>Error message</returns>
    private async Task<string> GetFullErrorTextAndRollbackEntityChangesAsync(DbUpdateException exception)
    {
        //rollback entity changes
        if (_context is DbContext dbContext)
        {
            var entries = dbContext.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

            entries.ForEach(entry => entry.State = EntityState.Unchanged);
        }

        await _context.SaveChangesAsync();
        return exception.ToString();
    }

    #endregion
}