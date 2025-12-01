using ByteDigest.Domain.Entities;
using ByteDigest.Domain.Interfaces;
using ByteDigest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ByteDigest.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Post entities.
/// </summary>
public class PostRepository : Repository<Post>, IPostRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PostRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public PostRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Gets a post by its slug.
    /// </summary>
    /// <param name="slug">The post slug.</param>
    /// <returns>The post if found; otherwise, null.</returns>
    public async Task<Post?> GetBySlugAsync(string slug)
    {
        return await DbSet.FirstOrDefaultAsync(p => p.Slug == slug);
    }

    /// <summary>
    /// Gets paginated published posts with optional filtering.
    /// </summary>
    /// <param name="page">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <param name="search">Optional search term.</param>
    /// <param name="category">Optional category filter.</param>
    /// <returns>A tuple containing the posts and total count.</returns>
    public async Task<(IEnumerable<Post> Posts, int TotalCount)> GetPublishedPostsAsync(
        int page, 
        int pageSize, 
        string? search = null, 
        string? category = null)
    {
        IQueryable<Post> query = DbSet.Where(p => p.IsPublished);

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => 
                p.Title.Contains(search) || 
                p.Excerpt.Contains(search) || 
                p.Content.Contains(search));
        }

        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(p => p.Category == category);
        }

        int totalCount = await query.CountAsync();
        
        List<Post> posts = await query
            .OrderByDescending(p => p.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (posts, totalCount);
    }
}
