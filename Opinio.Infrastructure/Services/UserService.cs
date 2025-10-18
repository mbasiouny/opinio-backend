using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Opinio.Core.Entities;
using Opinio.Core.Enums;
using Opinio.Core.Helpers;
using Opinio.Core.Repositories;
using Opinio.Core.Services;
using Opinio.Infrastructure.Extensions;


namespace Opinio.Infrastructure.Services;

public class UserService(IUserRepository userRepository, IValidator<User> validator, IConfiguration config) : IUserService
{
    #region Register
    public async Task<OperationResult<User>> RegisterAsync(User user, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(user, cancellationToken);
            if (!validationResult.IsValid)
                return OperationResult<User>.ValidationError(validationResult.ToErrorDictionary());

            if (await userRepository.IsExistAsync(user.Email, cancellationToken))
            {
                return OperationResultHelper.CreateValidationError<User>(nameof(user.Email), "This Email Already Exist");
            }

            var (salt, hash) = PasswordHasher.HashPassword(user.Password);

            user.Password = hash;
            user.Salt = salt;
            user.Role = (byte)UserRole.User;
            user.CreatedAt = DateTime.UtcNow;

            await userRepository.CreateAsync(user, cancellationToken);
            await userRepository.SaveChangesAsync(cancellationToken);

            return OperationResult<User>.Success(user, "User Created Successfully");
        }
        catch (Exception ex)
        {
            return OperationResult<User>.Failure(message: "Error When Create User");
        }
    }
    #endregion
    #region Login
    public async Task<OperationResult<string>> LoginAsync(User userReq, CancellationToken cancellationToken)
    {
        try
        {
            var userDb = await userRepository.GetByEmailAsync(userReq.Email, cancellationToken);
            if (userDb == null || !PasswordHasher.VerifyPassword(userReq.Password, userDb.Salt, userDb.Password))
                return OperationResult<string>.ValidationError(new() { { "credentials", new() { "Invalid username or password" } } });

            var token = GenerateJwtToken(userDb);
            return OperationResult<string>.Success(token);
        }
        catch (Exception ex)
        {
            return OperationResult<string>.Failure(message: "Error When Login");
        }

    }
    #endregion
    #region Logout
    public async Task<OperationResult<string>> LogoutAsync(ClaimsPrincipal user, CancellationToken ct)
    {
        try
        {
            var jti = user.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            var expClaim = user.FindFirst(JwtRegisteredClaimNames.Exp)?.Value;

            if (string.IsNullOrEmpty(jti) || string.IsNullOrEmpty(expClaim) || !long.TryParse(expClaim, out var exp))
                return OperationResult<string>.ValidationError(new() { { "token", new() { "Invalid or missing token data" } } });

            var expiresAt = DateTimeOffset.FromUnixTimeSeconds(exp).UtcDateTime;

            var jwtBlacklist = new JwtBlacklist { Jti = jti, ExpiresAt = expiresAt, RevokedAt = DateTime.UtcNow };
            await userRepository.RevokeTokenAsync(jwtBlacklist, ct);
            await userRepository.SaveChangesAsync();

            return OperationResult<string>.Success("Logged out successfully");
        }
        catch (Exception ex)
        {
            return OperationResult<string>.Failure(message: "Error When Logout");
        }

    }
    #endregion
    #region Private
    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim("userId", user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var jti = Guid.NewGuid().ToString();
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, jti));

        var expires = DateTime.UtcNow.AddHours(3);

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    #endregion
}
