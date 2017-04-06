using TOF.Core.Abstractions;
using System;
using System.Linq;

namespace TOF.Core.Utils.TypeConverters
{
    public class ByteArrayConverter : ITypeConverter
    {
        public object Convert(object ValueToConvert)
        {
            if (ValueToConvert == null || ValueToConvert == DBNull.Value)
                return new byte[] { };

            if (ValueToConvert.GetType() == typeof(string))
            {
                if (string.IsNullOrEmpty(ValueToConvert.ToString()))
                    return new byte[] { };
                else
                    return System.Convert.FromBase64String(ValueToConvert.ToString());
            }
            else
            {
                return (byte[])ValueToConvert;
            }
        }

        public bool IsEqual(object Value1, object Value2)
        {
            byte[] data1 = (byte[])Convert(Value1);
            byte[] data2 = (byte[])Convert(Value2);

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
