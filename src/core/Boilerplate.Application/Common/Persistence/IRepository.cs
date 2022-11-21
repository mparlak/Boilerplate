namespace Boilerplate.Application.Common.Persistence;

public interface IRepository<TEntity>
{
    /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        TEntity? GetById(object id);
        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        Task<TEntity?> GetByIdAsync(object id);
        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        Task InsertAsync(TEntity entity);
        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void BulkInsert(IEnumerable<TEntity> entities);


        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        Task BulkInsertAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(TEntity entity);
        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        Task UpdateAsync(TEntity entity);
        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void BulkUpdate(IEnumerable<TEntity> entities);

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        Task BulkUpdateAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void BulkDelete(IEnumerable<TEntity> entities);

        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<TEntity> GetAllAsNoTracking();

        /// <summary>
        /// Gets a table with paged response
        /// </summary>
        IQueryable<TEntity> GetAllWithPagination(int pageNumber, int pageSize);

        /// <summary>
        ///save context changes
        /// </summary>
        Task SaveAsync();
}
