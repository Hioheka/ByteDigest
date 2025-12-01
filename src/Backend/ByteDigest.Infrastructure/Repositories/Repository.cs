using System.Linq.Expressions;
using ByteDigest.Domain.Interfaces;
using ByteDigest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ByteDigest.Infrastructure.Repositories;

/// <summary>
/// Generic repository implementation for data access.
/// </summary>
/// <typeparam name="T">The entity type.</typeparam>
public class Repository<T> : IRepository<T> where T : class
{
    /// <summary>
    /// The database context.
    /// </summary>
    protected readonly ApplicationDbContext Context;

    /// <summary>
    /// The DbSet for the entity type.
    /// </summary>
    protected readonly DbSet<T> DbSet;

    /// <summary>
    /// Initializes a new instance of the <see cref="Repository{T}"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public Repository(ApplicationDbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    /// <summary>
    /// Gets an entity by its identifier.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await DbSet.FindAsync(id);
    }

    /// <summary>
    /// Gets all entities.
    /// </summary>
    /// <returns>A collection of all entities.</returns>
    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    /// <summary>
    /// Finds entities matching the specified predicate.
    /// </summary>
    /// <param name="predicate">The search predicate.</param>
    /// <returns>A collection of matching entities.</returns>
    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await DbSet.Where(predicate).ToListAsync();
    }

    /// <summary>
    /// Adds a new entity.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    public virtual async Task AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
    }

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    public virtual void Update(T entity)
    {
        DbSet.Update(entity);
    }

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    public virtual void Delete(T entity)
    {
        DbSet.Remove(entity);
    }

    /// <summary>
    /// Saves all changes to the database.
    /// </summary>
    /// <returns>The number of affected records.</returns>
    public virtual async Task<int> SaveChangesAsync()
    {
        return await Context.SaveChangesAsync();
    }
}
