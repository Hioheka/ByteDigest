using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ByteDigest.Application.DTOs;
using ByteDigest.Application.Interfaces;
using ByteDigest.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ByteDigest.Application.Services;

/// <summary>
/// Implements authentication and authorization operations.
/// </summary>
public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthService"/> class.
    /// </summary>
    /// <param name="userManager">The user manager.</param>
    /// <param name="configuration">The configuration.</param>
    public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    /// <summary>
    /// Authenticates a user and returns a JWT token.
    /// </summary>
    /// <param name="loginRequest">The login request data.</param>
    /// <returns>The login response with token if successful; otherwise, null.</returns>
    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginRequest)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(loginRequest.UserNameOrEmail) 
            ?? await _userManager.FindByNameAsync(loginRequest.UserNameOrEmail);

        if (user == null)
        {
            return null;
        }

        bool isPasswordValid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

        if (!isPasswordValid)
        {
            return null;
        }

        IList<string> userRoles = await _userManager.GetRolesAsync(user);
        string role = userRoles.FirstOrDefault() ?? "User";

        string token = GenerateJwtToken(user, role);

        return new LoginResponseDto
        {
            Token = token,
            UserName = user.UserName ?? string.Empty,
            Email = user.Email ?? string.Empty,
            Role = role
        };
    }

    /// <summary>
    /// Generates a JWT token for the authenticated user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="role">The user's role.</param>
    /// <returns>The JWT token.</returns>
    private string GenerateJwtToken(ApplicationUser user, string role)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
            new Claim(ClaimTypes.Role, role)
        };

        string? jwtKey = _configuration["Jwt:Key"];
        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new InvalidOperationException("JWT Key is not configured");
        }

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
