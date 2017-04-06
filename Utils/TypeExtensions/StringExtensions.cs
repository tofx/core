using System;
using System.Linq;
using System.Security;
using System.Text;

namespace TOF.Core.Utils.TypeExtensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static byte[] ToByteArray(this string str, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            return encoding.GetBytes(str);
        }

        public static byte[] FromBinaryStringToByteArray(this string str)
        {
            if (str == null)
                return null;

            return Enumerable.Range(0, str.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(str.Substring(x, 2), 16))
                .ToArray();
        }

        public static byte[] FromBase64UrlEncodedToDecodedByteArray(this string str)
        {
            string s = str;
            
            s = s.Replace('-', '+'); // 62nd char of encoding
            s = s.Replace('_', '/'); // 63rd char of encoding

            switch (s.Length % 4) // Pad with trailing '='s
            {
                case 0:
                    break; // No pad chars in this case
                case 2:
                    s += "==";
                    break; // Two pad chars
                case 3:
                    s += "=";
                    break; // One pad char
                default:
                    throw new System.Exception("Illegal base64url string");
            }

            return Convert.FromBase64String(s); // Standard base64 decoder
        }

        public static SecureString ToSecureString(this string password)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            var secureStr = new SecureString();

            foreach (char c in password)
                secureStr.AppendChar(c);

            return secureStr;
        }

        public static bool FoundInValues(this string str, params string[] Values)
        {
            if (Values == null || Values.Length == 0)
                throw new ArgumentException("NoValuesFound");

            foreach (var v in Values)
            {
                if (v == str)
                    return true;
            }

            return false;
        }

        public static bool ContainsInValues(this string str, params string[] Values)
        {
            if (Values == null || Values.Length == 0)
                throw new ArgumentException("NoValuesFound");

            foreach (var v in Values)
            {
                if (str.Contains(v))
                    return true;
            }

            return false;
        }
    }
}
