namespace ByteDigest.Domain.Entities;

/// <summary>
/// Represents a static page entity.
/// </summary>
public class Page
{
    /// <summary>
    /// Gets or sets the unique identifier for the page.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the URL-friendly slug for the page.
    /// </summary>
    public string Slug { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the title of the page.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the HTML content of the page.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the page is published.
    /// </summary>
    public bool IsPublished { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the page was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the page was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
