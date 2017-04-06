using tofx.Core.Abstractions;
using System;

namespace tofx.Core.Utils.TypeConverters
{
    public class DoubleConverter : ITypeConverter
    {
        public object Convert(object valueToConvert)
        {
            if (valueToConvert == null || valueToConvert == DBNull.Value)
                return 0.0d;

            return System.Convert.ToDouble(valueToConvert);
        }

        public bool IsEqual(object value1, object value2)
        {
            var v1 = (double)Convert(value1);
            var v2 = (double)Convert(value2);

            return v1 == v2;
        }
        
        public Type GetCompatibleType()
        {
            return typeof(double);
        }
    }
}
