using tofx.Core.Abstractions;
using System;

namespace tofx.Core.Utils.TypeConverters
{
    public class IntegerConverter : ITypeConverter
    {
        public object Convert(object valueToConvert) 
        {
            if (valueToConvert == null || valueToConvert == DBNull.Value)
                return 0;

            return System.Convert.ToInt32(valueToConvert);
        }

        public bool IsEqual(object value1, object value2)
        {
            var v1 = (int)Convert(value1);
            var v2 = (int)Convert(value2);

            return v1 == v2;
        }

        public Type GetCompatibleType()
        {
            return typeof(int);
        }
    }
}
