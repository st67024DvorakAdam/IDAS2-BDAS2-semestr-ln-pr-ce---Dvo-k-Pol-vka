using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Tools
{
    public static class PasswordHasher
    {

        public static string GenerateSalt(int size = 32)
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var saltBytes = new byte[size];
                randomNumberGenerator.GetBytes(saltBytes);
                return Convert.ToBase64String(saltBytes);
            }
        }


        public static string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = Encoding.UTF8.GetBytes(password + salt);
                var hash = sha256.ComputeHash(saltedPassword);
                return Convert.ToBase64String(hash);
            }
        }


        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var hashOfEnteredPassword = HashPassword(enteredPassword, storedSalt);
            return hashOfEnteredPassword == storedHash;
        }
    }
}