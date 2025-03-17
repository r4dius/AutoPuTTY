using System;
using System.Security.Cryptography;
using System.Text;

public static class Crypto
{
    // Constants for salt and hash sizes (in bytes)
    private const int SaltSize = 16; // 128-bit salt
    private const int HashSize = 32; // 256-bit hash
    private const int Iterations = 100000; // Number of iterations

    // Creates a hashed password string in the format: iterations.salt.hash
    public static string HashPassword(string password)
    {
        // Generate a random salt
        byte[] salt = new byte[SaltSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Derive the hash
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
        {
            byte[] hash = pbkdf2.GetBytes(HashSize);
            // Combine iterations, salt, and hash as a single string (Base64-encoded)
            return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }
    }

    // Verifies a password against the stored hash string.
    public static bool VerifyPassword(string password, string storedHash)
    {
        // Extract the parts: iterations, salt, and hash
        string[] parts = storedHash.Split('.');
        if (parts.Length != 3)
            return false;

        int iterations = int.Parse(parts[0]);
        byte[] salt = Convert.FromBase64String(parts[1]);
        byte[] storedPasswordHash = Convert.FromBase64String(parts[2]);

        // Derive the hash from the input password using the same salt and iteration count
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
        {
            byte[] computedHash = pbkdf2.GetBytes(HashSize);
            return AreHashesEqual(storedPasswordHash, computedHash);
        }
    }

    // Compares two byte arrays in constant time.
    private static bool AreHashesEqual(byte[] hash1, byte[] hash2)
    {
        uint diff = (uint)hash1.Length ^ (uint)hash2.Length;
        for (int i = 0; i < hash1.Length && i < hash2.Length; i++)
        {
            diff |= (uint)(hash1[i] ^ hash2[i]);
        }
        return diff == 0;
    }

    public static string MD5Hash(string input)
    {
        // convert the input string to a byte array and compute the hash.
        using (MD5 Md5Hash = MD5.Create())
        {
            byte[] Data = Md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // create a new StringBuilder to collect the bytes and create a string.
            StringBuilder StringBuilder = new StringBuilder();

            // loop through each byte of the hashed data and format each one as a hexadecimal string.
            for (int i = 0; i < Data.Length; i++)
            {
                StringBuilder.Append(Data[i].ToString("x2"));
            }

            // return the hexadecimal string.
            return StringBuilder.ToString();
        }
    }
}