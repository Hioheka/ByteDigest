namespace ByteDigest.Domain.Entities;

/// <summary>
/// Represents a blog post entity.
/// </summary>
public class Post
{
    /// <summary>
    /// Gets or sets the unique identifier for the post.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the URL-friendly slug for the post.
    /// </summary>
    public string Slug { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the title of the post.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the brief summary or excerpt of the post.
    /// </summary>
    public string Excerpt { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the full HTML content of the post.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the post.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the comma-separated tags for the post.
    /// </summary>
    public string Tags { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the post is published.
    /// </summary>
    public bool IsPublished { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the post was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the post was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
