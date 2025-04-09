using AutoPuTTY.Properties;
using System;
using System.Security.Cryptography;
using System.Text;

public static class Legacy
{
    public static string Decrypt(string encrypted, string key)
    {
        if (encrypted == "") return "";

        byte[] EncryptedArray = Convert.FromBase64String(encrypted);
        MD5CryptoServiceProvider Md5Hash = new MD5CryptoServiceProvider();
        byte[] KeyArray = Md5Hash.ComputeHash(Encoding.UTF8.GetBytes(key));

        TripleDESCryptoServiceProvider TripleDes = new TripleDESCryptoServiceProvider
        {
            Key = KeyArray,
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        };

        ICryptoTransform CryptoTransform = TripleDes.CreateDecryptor();
        byte[] Result = CryptoTransform.TransformFinalBlock(EncryptedArray, 0, EncryptedArray.Length);

        Md5Hash.Clear();
        TripleDes.Clear();

        return Encoding.UTF8.GetString(Result);
    }

    public static string Decrypt(string encrypted)
    {
        return Decrypt(encrypted, Settings.Default.cryptokey);
    }

    public static string Encrypt(string plain, string key)
    {
        if (plain == "") return "";

        byte[] PlainArray = Encoding.UTF8.GetBytes(plain);
        MD5CryptoServiceProvider Md5Hash = new MD5CryptoServiceProvider();
        byte[] KeyArray = Md5Hash.ComputeHash(Encoding.UTF8.GetBytes(key));

        TripleDESCryptoServiceProvider TripleDes = new TripleDESCryptoServiceProvider
        {
            Key = KeyArray,
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        };

        ICryptoTransform CryptoTransform = TripleDes.CreateEncryptor();
        byte[] Result = CryptoTransform.TransformFinalBlock(PlainArray, 0, PlainArray.Length);

        Md5Hash.Clear();
        TripleDes.Clear();

        return Convert.ToBase64String(Result, 0, Result.Length);
    }

    public static string Encrypt(string plain)
    {
        return Encrypt(plain, Settings.Default.cryptokey);
    }

    /// <summary>
    /// Computes the MD5 hash of a string and returns it as a hex string.
    /// </summary>
    /// <param name="input">Input string to hash.</param>
    /// <returns>MD5 hash as a lowercase hex string.</returns>
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