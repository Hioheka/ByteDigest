using ByteDigest.Application.DTOs;
using ByteDigest.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteDigest.Api.Controllers;

/// <summary>
/// Handles public post operations.
/// </summary>
[ApiController]
[Route("api/posts")]
[AllowAnonymous]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;

    /// <summary>
    /// Initializes a new instance of the <see cref="PostsController"/> class.
    /// </summary>
    /// <param name="postService">The post service.</param>
    public PostsController(IPostService postService)
    {
        _postService = postService;
    }

    /// <summary>
    /// Gets a paginated list of published posts.
    /// </summary>
    /// <param name="page">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <param name="search">Optional search term.</param>
    /// <param name="category">Optional category filter.</param>
    /// <returns>A paginated list of published posts.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResultDto<PostDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedResultDto<PostDto>>> GetPublishedPosts(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null,
        [FromQuery] string? category = null)
    {
        PaginatedResultDto<PostDto> result = await _postService.GetPublishedPostsAsync(page, pageSize, search, category);
        return Ok(result);
    }

    /// <summary>
    /// Gets a published post by its slug.
    /// </summary>
    /// <param name="slug">The post slug.</param>
    /// <returns>The published post.</returns>
    [HttpGet("{slug}")]
    [ProducesResponseType(typeof(PostDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PostDto>> GetPublishedPostBySlug(string slug)
    {
        PostDto? post = await _postService.GetPublishedPostBySlugAsync(slug);

        if (post == null)
        {
            return NotFound();
        }

        return Ok(post);
    }
}
