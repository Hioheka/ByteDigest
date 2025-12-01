using ByteDigest.Application.DTOs;
using ByteDigest.Application.Interfaces;
using ByteDigest.Domain.Entities;
using ByteDigest.Domain.Interfaces;

namespace ByteDigest.Application.Services;

/// <summary>
/// Implements page-related business logic and operations.
/// </summary>
public class PageService : IPageService
{
    private readonly IPageRepository _pageRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="PageService"/> class.
    /// </summary>
    /// <param name="pageRepository">The page repository.</param>
    public PageService(IPageRepository pageRepository)
    {
        _pageRepository = pageRepository;
    }

    /// <summary>
    /// Gets a published page by its slug.
    /// </summary>
    /// <param name="slug">The page slug.</param>
    /// <returns>The page if found; otherwise, null.</returns>
    public async Task<PageDto?> GetPublishedPageBySlugAsync(string slug)
    {
        Page? page = await _pageRepository.GetBySlugAsync(slug);

        if (page == null || !page.IsPublished)
        {
            return null;
        }

        return MapToDto(page);
    }

    /// <summary>
    /// Gets all pages for admin view.
    /// </summary>
    /// <returns>A collection of all pages.</returns>
    public async Task<IEnumerable<PageDto>> GetAllPagesAsync()
    {
        IEnumerable<Page> pages = await _pageRepository.GetAllAsync();
        return pages.Select(MapToDto);
    }

    /// <summary>
    /// Gets a page by its identifier.
    /// </summary>
    /// <param name="id">The page identifier.</param>
    /// <returns>The page if found; otherwise, null.</returns>
    public async Task<PageDto?> GetPageByIdAsync(int id)
    {
        Page? page = await _pageRepository.GetByIdAsync(id);
        return page != null ? MapToDto(page) : null;
    }

    /// <summary>
    /// Creates a new page.
    /// </summary>
    /// <param name="createPageDto">The page creation data.</param>
    /// <returns>The created page.</returns>
    public async Task<PageDto> CreatePageAsync(CreatePageDto createPageDto)
    {
        Page page = new Page
        {
            Slug = createPageDto.Slug,
            Title = createPageDto.Title,
            Content = createPageDto.Content,
            IsPublished = createPageDto.IsPublished,
            CreatedAt = DateTime.UtcNow
        };

        await _pageRepository.AddAsync(page);
        await _pageRepository.SaveChangesAsync();

        return MapToDto(page);
    }

    /// <summary>
    /// Updates an existing page.
    /// </summary>
    /// <param name="id">The page identifier.</param>
    /// <param name="updatePageDto">The page update data.</param>
    /// <returns>The updated page if found; otherwise, null.</returns>
    public async Task<PageDto?> UpdatePageAsync(int id, UpdatePageDto updatePageDto)
    {
        Page? page = await _pageRepository.GetByIdAsync(id);

        if (page == null)
        {
            return null;
        }

        page.Slug = updatePageDto.Slug;
        page.Title = updatePageDto.Title;
        page.Content = updatePageDto.Content;
        page.IsPublished = updatePageDto.IsPublished;
        page.UpdatedAt = DateTime.UtcNow;

        _pageRepository.Update(page);
        await _pageRepository.SaveChangesAsync();

        return MapToDto(page);
    }

    /// <summary>
    /// Deletes a page.
    /// </summary>
    /// <param name="id">The page identifier.</param>
    /// <returns>True if deleted; otherwise, false.</returns>
    public async Task<bool> DeletePageAsync(int id)
    {
        Page? page = await _pageRepository.GetByIdAsync(id);

        if (page == null)
        {
            return false;
        }

        _pageRepository.Delete(page);
        await _pageRepository.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Maps a Page entity to PageDto.
    /// </summary>
    /// <param name="page">The page entity.</param>
    /// <returns>The page DTO.</returns>
    private static PageDto MapToDto(Page page)
    {
        return new PageDto
        {
            Id = page.Id,
            Slug = page.Slug,
            Title = page.Title,
            Content = page.Content,
            IsPublished = page.IsPublished,
            CreatedAt = page.CreatedAt,
            UpdatedAt = page.UpdatedAt
        };
    }
}
