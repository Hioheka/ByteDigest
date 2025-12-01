using ByteDigest.Domain.Entities;

namespace ByteDigest.Domain.Interfaces;

/// <summary>
/// Defines repository operations specific to Post entities.
/// </summary>
public interface IPostRepository : IRepository<Post>
{
    /// <summary>
    /// Gets a post by its slug.
    /// </summary>
    /// <param name="slug">The post slug.</param>
    /// <returns>The post if found; otherwise, null.</returns>
    Task<Post?> GetBySlugAsync(string slug);

    /// <summary>
    /// Gets paginated published posts with optional filtering.
    /// </summary>
    /// <param name="page">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <param name="search">Optional search term.</param>
    /// <param name="category">Optional category filter.</param>
    /// <returns>A tuple containing the posts and total count.</returns>
    Task<(IEnumerable<Post> Posts, int TotalCount)> GetPublishedPostsAsync(
        int page, 
        int pageSize, 
        string? search = null, 
        string? category = null);
}
