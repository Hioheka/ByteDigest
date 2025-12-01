using Microsoft.AspNetCore.Identity;

namespace ByteDigest.Domain.Entities;

/// <summary>
/// Represents an application user with authentication and role information.
/// </summary>
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Gets or sets the full name of the user.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date and time when the user was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
