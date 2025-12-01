using ByteDigest.Application.DTOs;

namespace ByteDigest.Application.Interfaces;

/// <summary>
/// Defines the service contract for Page operations.
/// </summary>
public interface IPageService
{
    /// <summary>
    /// Gets a published page by its slug.
    /// </summary>
    /// <param name="slug">The page slug.</param>
    /// <returns>The page if found; otherwise, null.</returns>
    Task<PageDto?> GetPublishedPageBySlugAsync(string slug);

    /// <summary>
    /// Gets all pages for admin view.
    /// </summary>
    /// <returns>A collection of all pages.</returns>
    Task<IEnumerable<PageDto>> GetAllPagesAsync();

    /// <summary>
    /// Gets a page by its identifier.
    /// </summary>
    /// <param name="id">The page identifier.</param>
    /// <returns>The page if found; otherwise, null.</returns>
    Task<PageDto?> GetPageByIdAsync(int id);

    /// <summary>
    /// Creates a new page.
    /// </summary>
    /// <param name="createPageDto">The page creation data.</param>
    /// <returns>The created page.</returns>
    Task<PageDto> CreatePageAsync(CreatePageDto createPageDto);

    /// <summary>
    /// Updates an existing page.
    /// </summary>
    /// <param name="id">The page identifier.</param>
    /// <param name="updatePageDto">The page update data.</param>
    /// <returns>The updated page if found; otherwise, null.</returns>
    Task<PageDto?> UpdatePageAsync(int id, UpdatePageDto updatePageDto);

    /// <summary>
    /// Deletes a page.
    /// </summary>
    /// <param name="id">The page identifier.</param>
    /// <returns>True if deleted; otherwise, false.</returns>
    Task<bool> DeletePageAsync(int id);
}
