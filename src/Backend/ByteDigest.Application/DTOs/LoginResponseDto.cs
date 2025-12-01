namespace ByteDigest.Application.DTOs;

/// <summary>
/// Data transfer object for login responses.
/// </summary>
public class LoginResponseDto
{
    /// <summary>
    /// Gets or sets the JWT token.
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's role.
    /// </summary>
    public string Role { get; set; } = string.Empty;
}
