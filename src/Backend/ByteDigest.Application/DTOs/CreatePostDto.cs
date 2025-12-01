namespace ByteDigest.Application.DTOs;

/// <summary>
/// Data transfer object for creating a post.
/// </summary>
public class CreatePostDto
{
    /// <summary>
    /// Gets or sets the post slug.
    /// </summary>
    public string Slug { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the post title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the post excerpt.
    /// </summary>
    public string Excerpt { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the post content.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the post category.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the post tags.
    /// </summary>
    public string Tags { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether the post is published.
    /// </summary>
    public bool IsPublished { get; set; }
}
