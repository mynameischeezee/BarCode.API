using System;
using System.Security.Cryptography;

namespace Barcode.Services.Implementations.Helpers
{
    public static class Hasher
    {
        public static string GetHash(string salt, string password)
        {
            var saltConverted = Convert.FromBase64String(salt);
            var pbkdf2 = new Rfc2898DeriveBytes(password, saltConverted, 1000);
            byte[] hash = pbkdf2.GetBytes(20);
            
            byte[] hashBytes = new byte[36];
            Array.Copy(saltConverted, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            
            return Convert.ToBase64String(hashBytes);
        }

        public static string GetSalt()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            return Convert.ToBase64String(salt);
        }
    }
}