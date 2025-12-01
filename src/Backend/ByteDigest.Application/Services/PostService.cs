using ByteDigest.Application.DTOs;
using ByteDigest.Application.Interfaces;
using ByteDigest.Domain.Entities;
using ByteDigest.Domain.Interfaces;

namespace ByteDigest.Application.Services;

/// <summary>
/// Implements post-related business logic and operations.
/// </summary>
public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="PostService"/> class.
    /// </summary>
    /// <param name="postRepository">The post repository.</param>
    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    /// <summary>
    /// Gets a paginated list of published posts with optional filtering.
    /// </summary>
    /// <param name="page">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <param name="search">Optional search term.</param>
    /// <param name="category">Optional category filter.</param>
    /// <returns>A paginated result of posts.</returns>
    public async Task<PaginatedResultDto<PostDto>> GetPublishedPostsAsync(
        int page, 
        int pageSize, 
        string? search = null, 
        string? category = null)
    {
        (IEnumerable<Post> posts, int totalCount) = await _postRepository.GetPublishedPostsAsync(page, pageSize, search, category);

        IEnumerable<PostDto> postDtos = posts.Select(MapToDto);

        return new PaginatedResultDto<PostDto>
        {
            Items = postDtos,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    /// <summary>
    /// Gets a published post by its slug.
    /// </summary>
    /// <param name="slug">The post slug.</param>
    /// <returns>The post if found; otherwise, null.</returns>
    public async Task<PostDto?> GetPublishedPostBySlugAsync(string slug)
    {
        Post? post = await _postRepository.GetBySlugAsync(slug);

        if (post == null || !post.IsPublished)
        {
            return null;
        }

        return MapToDto(post);
    }

    /// <summary>
    /// Gets all posts for admin view.
    /// </summary>
    /// <returns>A collection of all posts.</returns>
    public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
    {
        IEnumerable<Post> posts = await _postRepository.GetAllAsync();
        return posts.Select(MapToDto);
    }

    /// <summary>
    /// Gets a post by its identifier.
    /// </summary>
    /// <param name="id">The post identifier.</param>
    /// <returns>The post if found; otherwise, null.</returns>
    public async Task<PostDto?> GetPostByIdAsync(int id)
    {
        Post? post = await _postRepository.GetByIdAsync(id);
        return post != null ? MapToDto(post) : null;
    }

    /// <summary>
    /// Creates a new post.
    /// </summary>
    /// <param name="createPostDto">The post creation data.</param>
    /// <returns>The created post.</returns>
    public async Task<PostDto> CreatePostAsync(CreatePostDto createPostDto)
    {
        Post post = new Post
        {
            Slug = createPostDto.Slug,
            Title = createPostDto.Title,
            Excerpt = createPostDto.Excerpt,
            Content = createPostDto.Content,
            Category = createPostDto.Category,
            Tags = createPostDto.Tags,
            IsPublished = createPostDto.IsPublished,
            CreatedAt = DateTime.UtcNow
        };

        await _postRepository.AddAsync(post);
        await _postRepository.SaveChangesAsync();

        return MapToDto(post);
    }

    /// <summary>
    /// Updates an existing post.
    /// </summary>
    /// <param name="id">The post identifier.</param>
    /// <param name="updatePostDto">The post update data.</param>
    /// <returns>The updated post if found; otherwise, null.</returns>
    public async Task<PostDto?> UpdatePostAsync(int id, UpdatePostDto updatePostDto)
    {
        Post? post = await _postRepository.GetByIdAsync(id);

        if (post == null)
        {
            return null;
        }

        post.Slug = updatePostDto.Slug;
        post.Title = updatePostDto.Title;
        post.Excerpt = updatePostDto.Excerpt;
        post.Content = updatePostDto.Content;
        post.Category = updatePostDto.Category;
        post.Tags = updatePostDto.Tags;
        post.IsPublished = updatePostDto.IsPublished;
        post.UpdatedAt = DateTime.UtcNow;

        _postRepository.Update(post);
        await _postRepository.SaveChangesAsync();

        return MapToDto(post);
    }

    /// <summary>
    /// Deletes a post.
    /// </summary>
    /// <param name="id">The post identifier.</param>
    /// <returns>True if deleted; otherwise, false.</returns>
    public async Task<bool> DeletePostAsync(int id)
    {
        Post? post = await _postRepository.GetByIdAsync(id);

        if (post == null)
        {
            return false;
        }

        _postRepository.Delete(post);
        await _postRepository.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Maps a Post entity to PostDto.
    /// </summary>
    /// <param name="post">The post entity.</param>
    /// <returns>The post DTO.</returns>
    private static PostDto MapToDto(Post post)
    {
        return new PostDto
        {
            Id = post.Id,
            Slug = post.Slug,
            Title = post.Title,
            Excerpt = post.Excerpt,
            Content = post.Content,
            Category = post.Category,
            Tags = post.Tags,
            IsPublished = post.IsPublished,
            CreatedAt = post.CreatedAt,
            UpdatedAt = post.UpdatedAt
        };
    }
}
