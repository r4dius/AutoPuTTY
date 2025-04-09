using AutoPuTTY.Properties;
using Konscious.Security.Cryptography;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class Crypto
{
    private const int SaltSize = 16; // The size of the salt (16 bytes)

    /// <summary>
    /// Generates a random salt.
    /// </summary>
    /// <returns>A random salt as a byte array.</returns>
    private static byte[] GenerateSalt()
    {
        byte[] salt = new byte[SaltSize];
        using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

    /// <summary>
    /// Extracts the salt from the combined hash and salt.
    /// </summary>
    /// <param name="hashWithSalt">The combined hash and salt.</param>
    /// <returns>The extracted salt as a byte array.</returns>
    private static byte[] ExtractSalt(byte[] hashWithSalt)
    {
        byte[] salt = new byte[SaltSize];
        Buffer.BlockCopy(hashWithSalt, 0, salt, 0, SaltSize);
        return salt;
    }

    /// <summary>
    /// Hashes a password using Argon2, embedding the salt within the hashed password.
    /// </summary>
    /// <param name="password">The password to hash.</param>
    /// <param name="storedSalt">The stored salt (optional, only used for verification).</param>
    /// <returns>The hashed password with the salt embedded as a Base64 string.</returns>
    public static string HashPassword(string password, byte[] storedSalt = null)
    {
        // Convert the password into bytes
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

        // Use the provided salt or generate a new one if null
        byte[] salt = storedSalt ?? GenerateSalt();

        // Argon2id - This is a specific version of Argon2 (you can also use Argon2d or Argon2i)
        using (var argon2 = new Argon2id(passwordBytes))
        {
            // Set the parameters (adjust these as needed for security/performance)
            argon2.Salt = salt;
            argon2.DegreeOfParallelism = Environment.ProcessorCount; // Number of threads
            argon2.Iterations = 4;                                  // The number of iterations
            argon2.MemorySize = 128 * 1024;                          // Memory in KB (64MB)

            // Get the resulting hash
            byte[] hash = argon2.GetBytes(32); // Generate a 32-byte hash (256 bits)

            // Combine the salt and the hash into a single byte array
            byte[] hashWithSalt = new byte[salt.Length + hash.Length];
            Buffer.BlockCopy(salt, 0, hashWithSalt, 0, salt.Length);
            Buffer.BlockCopy(hash, 0, hashWithSalt, salt.Length, hash.Length);

            // Return the result as a Base64 string (combining salt and hash)
            return Convert.ToBase64String(hashWithSalt);
        }
    }

    /// <summary>
    /// Verifies the password by comparing it with the stored hash, which contains the salt.
    /// </summary>
    /// <param name="password">The password entered by the user.</param>
    /// <param name="storedHash">The stored hash with the embedded salt.</param>
    /// <returns>True if the password is correct, false otherwise.</returns>
    public static bool VerifyPassword(string password, string storedHash)
    {
        if (password.Trim() == "" || storedHash.Trim() == "") return false;
        // Convert the stored hash from Base64 to byte array
        byte[] hashWithSalt = Convert.FromBase64String(storedHash);

        // Extract the salt from the stored hash
        byte[] salt = ExtractSalt(hashWithSalt);

        // Hash the password with the extracted salt
        string hashedPassword = HashPassword(password, salt);

        // Compare the computed hash with the stored hash
        return storedHash == hashedPassword;
    }

    private static byte[] DeriveKey(string password, byte[] salt, int keySize)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

        var argon2 = new Argon2id(passwordBytes)
        {
            Salt = salt,
            DegreeOfParallelism = Environment.ProcessorCount, // Number of threads to use.
            Iterations = 4,            // The number of iterations.
            MemorySize = 64 * 1024     // Memory size in kilobytes (this example uses ~64MB).
        };

        return argon2.GetBytes(keySize);
    }

    public static string Encrypt(string plain)
    {
        return plain.Trim() != "" ? Encrypt(plain, Settings.Default.cryptokey) : "";
    }

    /// <summary>
    /// Encrypts a string using AES with a key derived from the supplied password.
    /// </summary>
    /// <param name="plain">The string to encrypt.</param>
    /// <param name="password">The password used for key derivation.</param>
    /// <returns>The encrypted data as a base64-encoded string.</returns>
    public static string Encrypt(string plain, string password)
    {
        using (var aes = new AesManaged())
        {
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            aes.GenerateIV();
            byte[] iv = aes.IV;

            byte[] salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            int keySizeBytes = aes.KeySize / 8;
            byte[] key = DeriveKey(password, salt, keySizeBytes);
            aes.Key = key;

            using (var ms = new MemoryStream())
            {
                ms.Write(salt, 0, salt.Length);
                ms.Write(iv, 0, iv.Length);

                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                using (var writer = new StreamWriter(cs))
                {
                    writer.Write(plain);
                }

                byte[] encrypted = ms.ToArray();

                // Return the result as a base64-encoded string
                return Convert.ToBase64String(encrypted);
            }
        }
    }

    public static string Decrypt(string encrypted)
    {
        return encrypted.Trim() != "" ? Decrypt(encrypted, Settings.Default.cryptokey) : "";
    }

    /// <summary>
    /// Decrypts a base64-encoded string that was encrypted with the Encrypt method.
    /// </summary>
    /// <param name="encrypted">The base64-encoded string to decrypt.</param>
    /// <param name="password">The password used for key derivation.</param>
    /// <returns>The decrypted string.</returns>
    public static string Decrypt(string encrypted, string password)
    {
        byte[] cipherData = Convert.FromBase64String(encrypted);

        byte[] salt = new byte[SaltSize];
        byte[] iv = new byte[SaltSize];
        Array.Copy(cipherData, 0, salt, 0, salt.Length);
        Array.Copy(cipherData, salt.Length, iv, 0, iv.Length);

        using (var aes = new AesManaged())
        {
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.IV = iv;

            int keySizeBytes = aes.KeySize / 8;
            byte[] key = DeriveKey(password, salt, keySizeBytes);
            aes.Key = key;

            using (var ms = new MemoryStream())
            {
                int ciphertextOffset = salt.Length + iv.Length;
                int ciphertextLength = cipherData.Length - ciphertextOffset;

                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherData, ciphertextOffset, ciphertextLength);
                    cs.FlushFinalBlock();
                }

                // Convert the decrypted data back to a string
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
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