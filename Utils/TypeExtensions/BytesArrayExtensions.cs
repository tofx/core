using System;
using System.Text;

namespace tofx.Core.Utils.TypeExtensions
{
    public static class BytesArrayExtensions
    {
        public static string ToHexString(this byte[] data, bool upperCase = false)
        {
            return (upperCase) 
                ? BitConverter.ToString(data).ToUpper().Replace("-", "")
                : BitConverter.ToString(data).ToLower().Replace("-", "");
        }

        public static string ToBase64UrlEncodedString(this byte[] data)
        {
            string s = Convert.ToBase64String(data); // Regular base64 encoder

            s = s.Split('=')[0]; // Remove any trailing '='s
            s = s.Replace('+', '-'); // 62nd char of encoding
            s = s.Replace('/', '_'); // 63rd char of encoding

            return s;
        }

        public static string ToByteString(this byte[] data, string pattern = "{0}")
        {
            var sb = new StringBuilder();

            foreach (var b in data)
                sb.Append(string.Format(pattern, b.ToString("X")));

            return sb.ToString();
        }

        public static byte[] Merge(this byte[] data, byte[] dataToMerge)
        {
            byte[] c = new byte[data.Length + dataToMerge.Length];
            Buffer.BlockCopy(data, 0, c, 0, data.Length);
            Buffer.BlockCopy(dataToMerge, 0, c, data.Length, dataToMerge.Length);
            return c;
        }

        public static bool IsAllBytesMatch(this byte[] data, byte[] dataToCompare)
        {
            if (data.Length != dataToCompare.Length)
                return false;

            for (int i = 0; i<data.Length; i++)
            {
                if (data[i] != dataToCompare[i])
                    return false;
            }

            return true;
        }

        public static bool IsNullOrEmpty(this byte[] data)
        {
            if (data == null)
                return true;
            if (data.Length == 0)
                return true;

            return false;
        }
    }
}
