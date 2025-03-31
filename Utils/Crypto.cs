using AutoPuTTY.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class Crypto
{
    // Constants for salt and hash sizes (in bytes)
    private const int SaltSize = 32; // 256-bit hash
    private const int HashSize = 32; // 256-bit hash
    private const int IvByteSize = 16;
    private const int Iterations = 300000; // Number of iterations
    private const int KeySize = 256;

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
            return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }
    }

    // Verifies a password against the stored hash string.
    public static bool VerifyPassword(string password, string storedHash)
    {
        // Extract the parts: iterations, salt, and hash
        string[] parts = storedHash.Split('.');
        if (parts.Length != 2)
            return false;

        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] storedPasswordHash = Convert.FromBase64String(parts[1]);

        // Derive the hash from the input password using the same salt and iteration count
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
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

    public static string Encrypt(string plain)
    {
        return plain.Trim() != "" ? Encrypt(plain, Settings.Default.cryptokey) : "";
    }

    /// <summary>
    /// Encrypts plain text using AES encryption with a passphrase.
    /// The output is a Base64 string containing the salt, IV, and ciphertext.
    /// </summary>
    public static string Encrypt(string plainText, string passphrase)
    {
        // Generate a random salt and IV.
        byte[] saltBytes = GenerateRandomBytes(SaltSize);
        byte[] ivBytes = GenerateRandomBytes(IvByteSize);
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

        // Derive a 256-bit key using PBKDF2.
        using (var keyDerivationFunction = new Rfc2898DeriveBytes(passphrase, saltBytes, Iterations, HashAlgorithmName.SHA256))
        {
            byte[] keyBytes = keyDerivationFunction.GetBytes(KeySize / 8);

            using (var aes = Aes.Create())
            {
                aes.KeySize = KeySize;
                aes.BlockSize = 128;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = keyBytes;
                aes.IV = ivBytes;

                using (var memoryStream = new MemoryStream())
                {
                    // Prepend the salt and IV to the output.
                    memoryStream.Write(saltBytes, 0, saltBytes.Length);
                    memoryStream.Write(ivBytes, 0, ivBytes.Length);

                    using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cryptoStream.FlushFinalBlock();

                        // Return the combined salt+IV+ciphertext as a Base64 string.
                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
        }

    }

    public static string Decrypt(string encrypted)
    {
        return encrypted.Trim() != "" ? Decrypt(encrypted, Settings.Default.cryptokey) : "";
    }

    /// <summary>
    /// Decrypts a Base64 string (produced by Encrypt) using the provided passphrase.
    /// </summary>
    public static string Decrypt(string cipherText, string passphrase)
    {
        byte[] cipherBytesWithSaltAndIv = Convert.FromBase64String(cipherText);

        // Extract the salt, IV, and ciphertext.
        byte[] saltBytes = new byte[SaltSize];
        Array.Copy(cipherBytesWithSaltAndIv, 0, saltBytes, 0, SaltSize);

        byte[] ivBytes = new byte[IvByteSize];
        Array.Copy(cipherBytesWithSaltAndIv, SaltSize, ivBytes, 0, IvByteSize);

        int cipherStartIndex = SaltSize + IvByteSize;
        int cipherLength = cipherBytesWithSaltAndIv.Length - cipherStartIndex;
        byte[] cipherBytes = new byte[cipherLength];
        Array.Copy(cipherBytesWithSaltAndIv, cipherStartIndex, cipherBytes, 0, cipherLength);

        // Derive the key from the passphrase and salt.
        using (var keyDerivationFunction = new Rfc2898DeriveBytes(passphrase, saltBytes, Iterations, HashAlgorithmName.SHA256))
        {
            byte[] keyBytes = keyDerivationFunction.GetBytes(KeySize / 8);

            using (var aes = Aes.Create())
            {
                aes.KeySize = KeySize;
                aes.BlockSize = 128;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = keyBytes;
                aes.IV = ivBytes;

                using (var memoryStream = new MemoryStream(cipherBytes))
                using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                using (var reader = new StreamReader(cryptoStream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }

    /// <summary>
    /// Generates a specified number of random bytes.
    /// </summary>
    private static byte[] GenerateRandomBytes(int size)
    {
        byte[] randomBytes = new byte[size];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }
        return randomBytes;
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