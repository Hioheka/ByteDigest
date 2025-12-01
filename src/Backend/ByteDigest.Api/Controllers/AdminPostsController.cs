using ByteDigest.Application.DTOs;
using ByteDigest.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteDigest.Api.Controllers;

/// <summary>
/// Handles administrative post operations.
/// </summary>
[ApiController]
[Route("api/admin/posts")]
[Authorize(Roles = "Admin")]
public class AdminPostsController : ControllerBase
{
    private readonly IPostService _postService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdminPostsController"/> class.
    /// </summary>
    /// <param name="postService">The post service.</param>
    public AdminPostsController(IPostService postService)
    {
        _postService = postService;
    }

    /// <summary>
    /// Gets all posts.
    /// </summary>
    /// <returns>A collection of all posts.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PostDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetAllPosts()
    {
        IEnumerable<PostDto> posts = await _postService.GetAllPostsAsync();
        return Ok(posts);
    }

    /// <summary>
    /// Gets a post by its identifier.
    /// </summary>
    /// <param name="id">The post identifier.</param>
    /// <returns>The post.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PostDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PostDto>> GetPostById(int id)
    {
        PostDto? post = await _postService.GetPostByIdAsync(id);

        if (post == null)
        {
            return NotFound();
        }

        return Ok(post);
    }

    /// <summary>
    /// Creates a new post.
    /// </summary>
    /// <param name="createPostDto">The post creation data.</param>
    /// <returns>The created post.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(PostDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PostDto>> CreatePost([FromBody] CreatePostDto createPostDto)
    {
        PostDto post = await _postService.CreatePostAsync(createPostDto);
        return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
    }

    /// <summary>
    /// Updates an existing post.
    /// </summary>
    /// <param name="id">The post identifier.</param>
    /// <param name="updatePostDto">The post update data.</param>
    /// <returns>The updated post.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(PostDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PostDto>> UpdatePost(int id, [FromBody] UpdatePostDto updatePostDto)
    {
        PostDto? post = await _postService.UpdatePostAsync(id, updatePostDto);

        if (post == null)
        {
            return NotFound();
        }

        return Ok(post);
    }

    /// <summary>
    /// Deletes a post.
    /// </summary>
    /// <param name="id">The post identifier.</param>
    /// <returns>No content on success.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePost(int id)
    {
        bool result = await _postService.DeletePostAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
