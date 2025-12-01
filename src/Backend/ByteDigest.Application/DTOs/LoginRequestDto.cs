namespace ByteDigest.Application.DTOs;

/// <summary>
/// Data transfer object for login requests.
/// </summary>
public class LoginRequestDto
{
    /// <summary>
    /// Gets or sets the username or email.
    /// </summary>
    public string UserNameOrEmail { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}
