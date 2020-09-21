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
        public static byte[] GenerateHash(string password, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;

                var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                return passwordHash;
            }
        }

        public static bool CheckPassword(User user, string password)
        {
            using (var hmac = new HMACSHA512(Convert.FromBase64String(user.PasswordSalt)))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                var passwordHash = Convert.FromBase64String(user.PasswordHash);

                return !computedHash.Where((t, i) => t != passwordHash[i]).Any();
            }
        }
    }
}
