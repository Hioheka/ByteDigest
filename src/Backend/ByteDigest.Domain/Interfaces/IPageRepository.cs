using ByteDigest.Domain.Entities;

namespace ByteDigest.Domain.Interfaces;

/// <summary>
/// Defines repository operations specific to Page entities.
/// </summary>
public interface IPageRepository : IRepository<Page>
{
    /// <summary>
    /// Gets a page by its slug.
    /// </summary>
    /// <param name="slug">The page slug.</param>
    /// <returns>The page if found; otherwise, null.</returns>
    Task<Page?> GetBySlugAsync(string slug);

    /// <summary>
    /// Gets all published pages.
    /// </summary>
    /// <returns>A collection of published pages.</returns>
    Task<IEnumerable<Page>> GetPublishedPagesAsync();
}
