using System;
using System.Security.Cryptography;

namespace UserManagementApp.Utils
{
    public static class PasswordHasher
    {
        // The following constants may be changed without breaking existing hashes.
        private const int SaltSize = 16; // size in bytes
        private const int HashSize = 20; // size in bytes
        private const int Iterations = 10000; // number of pbkdf2 iterations

        public static string HashPassword(string password)
        {
            // Generate a random salt
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Hash the password and encode the parameters
            byte[] hash = GetPbkdf2Bytes(password, salt, Iterations, HashSize);
            
            // Format: iterations.salt.hash
            return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Extract the parameters from the hash
            char[] delimiter = { '.' };
            string[] split = hashedPassword.Split(delimiter);
            
            int iterations = int.Parse(split[0]);
            byte[] salt = Convert.FromBase64String(split[1]);
            byte[] hash = Convert.FromBase64String(split[2]);

            byte[] testHash = GetPbkdf2Bytes(password, salt, iterations, hash.Length);
            
            // Compare the hashes in constant time to prevent timing attacks
            return SlowEquals(hash, testHash);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        private static byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return pbkdf2.GetBytes(outputBytes);
            }
        }
    }
}