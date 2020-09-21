using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using MyPortal.Database.Models;

namespace MyPortal.Logic.Authentication
{
    public class PasswordManager
    {
        private const int Iterations = 100000;

        public static string GenerateHash(string password)
        {
            byte[] salt;

            using (var cryptoSeviceProvider = new RNGCryptoServiceProvider())
            {
                cryptoSeviceProvider.GetBytes(salt = new byte[16]);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                byte[] hash = pbkdf2.GetBytes(20);

                byte[] hashBytes = new byte[36];

                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);

                return Convert.ToBase64String(hashBytes);
            }
        }

        public static bool CheckPassword(string passwordHash, string password)
        {
            byte[] hashBytes = Convert.FromBase64String(passwordHash);

            byte[] salt = new byte[16];

            Array.Copy(hashBytes, 0, salt, 0, 16);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                byte[] hash = pbkdf2.GetBytes(20);

                for (int i = 0; i < 20; i++)
                {
                    if (hashBytes[i + 16] != hash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
