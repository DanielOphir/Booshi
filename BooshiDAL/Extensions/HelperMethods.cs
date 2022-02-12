using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooshiDAL.Extensions
{
    public static class HelperMethods
    {
        /// <summary>
        /// Generating random password
        /// </summary>
        /// <param name="length">Length of generated password</param>
        /// <returns>string with the random password</returns>
        public static string GenerateRandomPassword(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890!@#$%^&*";
            string password = "";
            for (int i = 0; i < length; i++)
            {
                password += chars[new Random().Next(0, chars.Length - 1)];
            }
            return password;
        }
    }
}
