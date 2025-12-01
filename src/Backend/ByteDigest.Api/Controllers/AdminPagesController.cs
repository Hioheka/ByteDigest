using ByteDigest.Application.DTOs;
using ByteDigest.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteDigest.Api.Controllers;

/// <summary>
/// Handles administrative page operations.
/// </summary>
[ApiController]
[Route("api/admin/pages")]
[Authorize(Roles = "Admin")]
public class AdminPagesController : ControllerBase
{
    private readonly IPageService _pageService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdminPagesController"/> class.
    /// </summary>
    /// <param name="pageService">The page service.</param>
    public AdminPagesController(IPageService pageService)
    {
        _pageService = pageService;
    }

    /// <summary>
    /// Gets all pages.
    /// </summary>
    /// <returns>A collection of all pages.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PageDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PageDto>>> GetAllPages()
    {
        IEnumerable<PageDto> pages = await _pageService.GetAllPagesAsync();
        return Ok(pages);
    }

    /// <summary>
    /// Gets a page by its identifier.
    /// </summary>
    /// <param name="id">The page identifier.</param>
    /// <returns>The page.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PageDto>> GetPageById(int id)
    {
        PageDto? page = await _pageService.GetPageByIdAsync(id);

        if (page == null)
        {
            return NotFound();
        }

        return Ok(page);
    }

    /// <summary>
    /// Creates a new page.
    /// </summary>
    /// <param name="createPageDto">The page creation data.</param>
    /// <returns>The created page.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(PageDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PageDto>> CreatePage([FromBody] CreatePageDto createPageDto)
    {
        PageDto page = await _pageService.CreatePageAsync(createPageDto);
        return CreatedAtAction(nameof(GetPageById), new { id = page.Id }, page);
    }

    /// <summary>
    /// Updates an existing page.
    /// </summary>
    /// <param name="id">The page identifier.</param>
    /// <param name="updatePageDto">The page update data.</param>
    /// <returns>The updated page.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(PageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PageDto>> UpdatePage(int id, [FromBody] UpdatePageDto updatePageDto)
    {
        PageDto? page = await _pageService.UpdatePageAsync(id, updatePageDto);

        if (page == null)
        {
            return NotFound();
        }

        return Ok(page);
    }

    /// <summary>
    /// Deletes a page.
    /// </summary>
    /// <param name="id">The page identifier.</param>
    /// <returns>No content on success.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePage(int id)
    {
        bool result = await _pageService.DeletePageAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
