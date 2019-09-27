using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MelidyaEntity.Core
{
    public static class SecurityExtension
    {
        public static string RemoveInjection<T>(this T obj)
        {
            var builder = new StringBuilder(obj as string);
            builder.Replace("<", " ");
            builder.Replace(">", " ");
            builder.Replace("'", " ");
            builder.Replace("\"", " ");
            builder.Replace("  ", " ");
            return builder.ToString().TrimStart().TrimEnd();
        }

        public static string GetHash(this string str)
        {
            // First we need to convert the string into bytes, which
            // means using a text encoder.
            var enc = Encoding.Unicode.GetEncoder();

            // Create a buffer large enough to hold the string
            var unicodeText = new byte[str.Length * 2];
            enc.GetBytes(str.ToCharArray(), 0, str.Length, unicodeText, 0, true);

            // Now that we have a byte array we can ask the CSP to hash it
            MD5 md5 = new MD5CryptoServiceProvider();
            var result = md5.ComputeHash(unicodeText);

            // Build the final string by converting each byte
            // into hex and appending it to a StringBuilder
            var sb = new StringBuilder();
            for (var i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("X2"));
            }

            // And return it
            return sb.ToString();
        }

        public static string MD5Hash(this string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }

    public static class ConvertExtensionMethods
    {
        public static int ToInt32(this object obj)
        {
            return Convert.ToInt32(obj);
        }

        public static byte ToByte(this object obj)
        {
            return Convert.ToByte(obj);
        }

        public static double ToDouble(this object obj)
        {
            return Convert.ToDouble(obj);
        }

        public static decimal ToDecimal(this object obj)
        {
            string value = obj.ToString().Replace(",", ".");
            decimal returnValue = 0;
            decimal.TryParse(value, out returnValue);
            return returnValue;
        }

        public static bool ToBoolean(this object obj)
        {
            return Convert.ToBoolean(obj);
        }

        public static long ToLong(this object obj)
        {
            return Convert.ToInt64(obj);
        }

        public static string Reverse(this string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string ToShortString(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return "";
            return s.Length > 50 ? s.Substring(0, 50) : s;
        }
    }
}
