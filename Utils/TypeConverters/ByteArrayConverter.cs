using tofx.Core.Abstractions;
using System;
using System.Linq;

namespace tofx.Core.Utils.TypeConverters
{
    public class ByteArrayConverter : ITypeConverter
    {
        public object Convert(object valueToConvert)
        {
            if (valueToConvert == null || valueToConvert == DBNull.Value)
                return new byte[] { };

            if (valueToConvert.GetType() == typeof(string))
            {
                if (string.IsNullOrEmpty(valueToConvert.ToString()))
                    return new byte[] { };
                else
                    return System.Convert.FromBase64String(valueToConvert.ToString());
            }
            else
            {
                return (byte[])valueToConvert;
            }
        }

        public bool IsEqual(object value1, object value2)
        {
            byte[] data1 = (byte[])Convert(value1);
            byte[] data2 = (byte[])Convert(value2);

            if (data1.Length != data2.Length)
                return false;

            return data1.SequenceEqual(data2);
        }

        public Type GetCompatibleType()
        {
            return typeof(byte[]);
        }
    }
}
