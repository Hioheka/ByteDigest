using ByteDigest.Domain.Entities;
using ByteDigest.Domain.Interfaces;
using ByteDigest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ByteDigest.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Page entities.
/// </summary>
public class PageRepository : Repository<Page>, IPageRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PageRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public PageRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Gets a page by its slug.
    /// </summary>
    /// <param name="slug">The page slug.</param>
    /// <returns>The page if found; otherwise, null.</returns>
    public async Task<Page?> GetBySlugAsync(string slug)
    {
        return await DbSet.FirstOrDefaultAsync(p => p.Slug == slug);
    }

    /// <summary>
    /// Gets all published pages.
    /// </summary>
    /// <returns>A collection of published pages.</returns>
    public async Task<IEnumerable<Page>> GetPublishedPagesAsync()
    {
        return await DbSet
            .Where(p => p.IsPublished)
            .OrderBy(p => p.Title)
            .ToListAsync();
    }
}
