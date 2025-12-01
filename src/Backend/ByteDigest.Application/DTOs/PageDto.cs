namespace ByteDigest.Application.DTOs;

/// <summary>
/// Data transfer object for Page entity.
/// </summary>
public class PageDto
{
    /// <summary>
    /// Gets or sets the page identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the page slug.
    /// </summary>
    public string Slug { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the page title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the page content.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether the page is published.
    /// </summary>
    public bool IsPublished { get; set; }

    /// <summary>
    /// Gets or sets the creation date.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last update date.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
