using System.Security.Cryptography;
using System.Text;

namespace backend.Models.Utilities;

public class SecurePassword
{
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public bool Authorized { get; set; } = false;

    public SecurePassword(string password)
    {
        GenerateSecurePassword(password);
    }

    public SecurePassword(byte[] passwordHash, byte[] passwordSalt, string password)
    {
        Authorized = Authorize(passwordHash, passwordSalt, password);
    }

    private void GenerateSecurePassword(string password)
    {
        using var hmac = new HMACSHA512();
        PasswordSalt = hmac.Key;
        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private bool Authorize(byte[] passwordHash, byte[] passwordSalt, string password)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return hash.SequenceEqual(passwordHash);
    }
}
