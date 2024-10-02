using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace com.msc.infraestructure.utils
{
    public static class Encrypt
    {
        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public static string GetPasswordGenerated()
        {
            return Membership.GeneratePassword(4, 0);
        }

    }
}
