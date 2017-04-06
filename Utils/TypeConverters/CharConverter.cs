using tofx.Core.Abstractions;
using System;

namespace tofx.Core.Utils.TypeConverters
{
    public class CharConverter : ITypeConverter
    {
        public object Convert(object valueToConvert)
        {
            if (valueToConvert == null || valueToConvert == DBNull.Value)
                return (char)0x0;

            return System.Convert.ToChar(valueToConvert);
        }

        public bool IsEqual(object value1, object value2)
        {
            char v1 = (char)Convert(value1);
            char v2 = (char)Convert(value2);

            return v1 == v2;
        }
        
        public Type GetCompatibleType()
        {
            return typeof(char);
        }
    }
}
