using System.Security.Cryptography;

namespace Opinio.Core.Helpers;

public static class PasswordHasher
{
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int Iterations = 100_000;

    public static (string Salt, string Hash) HashPassword(string password)
    {
        var saltBytes = RandomNumberGenerator.GetBytes(SaltSize);
        var salt = Convert.ToBase64String(saltBytes);

        using var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256);
        var hash = Convert.ToBase64String(deriveBytes.GetBytes(KeySize));

        return (salt, hash);
    }

    public static bool VerifyPassword(string password, string salt, string hash)
    {
        var saltBytes = Convert.FromBase64String(salt);
        using var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256);
        var computed = Convert.ToBase64String(deriveBytes.GetBytes(KeySize));
        return CryptographicOperations.FixedTimeEquals(Convert.FromBase64String(computed), Convert.FromBase64String(hash));
    }
}