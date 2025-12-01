using ByteDigest.Application.DTOs;

namespace ByteDigest.Application.Interfaces;

/// <summary>
/// Defines the service contract for Post operations.
/// </summary>
public interface IPostService
{
    /// <summary>
    /// Gets a paginated list of published posts with optional filtering.
    /// </summary>
    /// <param name="page">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <param name="search">Optional search term.</param>
    /// <param name="category">Optional category filter.</param>
    /// <returns>A paginated result of posts.</returns>
    Task<PaginatedResultDto<PostDto>> GetPublishedPostsAsync(int page, int pageSize, string? search = null, string? category = null);

    /// <summary>
    /// Gets a published post by its slug.
    /// </summary>
    /// <param name="slug">The post slug.</param>
    /// <returns>The post if found; otherwise, null.</returns>
    Task<PostDto?> GetPublishedPostBySlugAsync(string slug);

    /// <summary>
    /// Gets all posts for admin view.
    /// </summary>
    /// <returns>A collection of all posts.</returns>
    Task<IEnumerable<PostDto>> GetAllPostsAsync();

    /// <summary>
    /// Gets a post by its identifier.
    /// </summary>
    /// <param name="id">The post identifier.</param>
    /// <returns>The post if found; otherwise, null.</returns>
    Task<PostDto?> GetPostByIdAsync(int id);

    /// <summary>
    /// Creates a new post.
    /// </summary>
    /// <param name="createPostDto">The post creation data.</param>
    /// <returns>The created post.</returns>
    Task<PostDto> CreatePostAsync(CreatePostDto createPostDto);

    /// <summary>
    /// Updates an existing post.
    /// </summary>
    /// <param name="id">The post identifier.</param>
    /// <param name="updatePostDto">The post update data.</param>
    /// <returns>The updated post if found; otherwise, null.</returns>
    Task<PostDto?> UpdatePostAsync(int id, UpdatePostDto updatePostDto);

    /// <summary>
    /// Deletes a post.
    /// </summary>
    /// <param name="id">The post identifier.</param>
    /// <returns>True if deleted; otherwise, false.</returns>
    Task<bool> DeletePostAsync(int id);
}
