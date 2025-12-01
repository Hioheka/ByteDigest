using ByteDigest.Application.DTOs;
using ByteDigest.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteDigest.Api.Controllers;

/// <summary>
/// Handles public page operations.
/// </summary>
[ApiController]
[Route("api/pages")]
[AllowAnonymous]
public class PagesController : ControllerBase
{
    private readonly IPageService _pageService;

    /// <summary>
    /// Initializes a new instance of the <see cref="PagesController"/> class.
    /// </summary>
    /// <param name="pageService">The page service.</param>
    public PagesController(IPageService pageService)
    {
        _pageService = pageService;
    }

    /// <summary>
    /// Gets a published page by its slug.
    /// </summary>
    /// <param name="slug">The page slug.</param>
    /// <returns>The published page.</returns>
    [HttpGet("{slug}")]
    [ProducesResponseType(typeof(PageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PageDto>> GetPublishedPageBySlug(string slug)
    {
        PageDto? page = await _pageService.GetPublishedPageBySlugAsync(slug);

        if (page == null)
        {
            return NotFound();
        }

        return Ok(page);
    }
}
