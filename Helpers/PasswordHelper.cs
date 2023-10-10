using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EmPoderTIC.Helpers
{
    public class PasswordHelper
    {
        public static void CreatePassword(string password, out byte[] passwordMash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordMash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool  CheckPassword(string password, byte[] passwordMash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                byte[] computePassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computePassword.Length; i++)
                    if (passwordMash[i] != computePassword[i])
                        return false;
            }

            return true;
        }
    }
}
