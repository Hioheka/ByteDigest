using ByteDigest.Application.DTOs;

namespace ByteDigest.Application.Interfaces;

/// <summary>
/// Defines the service contract for authentication operations.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Authenticates a user and returns a JWT token.
    /// </summary>
    /// <param name="loginRequest">The login request data.</param>
    /// <returns>The login response with token if successful; otherwise, null.</returns>
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginRequest);
}
