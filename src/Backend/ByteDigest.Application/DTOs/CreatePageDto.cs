namespace ByteDigest.Application.DTOs;

/// <summary>
/// Data transfer object for creating a page.
/// </summary>
public class CreatePageDto
{
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
}
