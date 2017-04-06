using tofx.Core.Abstractions;
using System;

namespace tofx.Core.Utils.TypeConverters
{
    public class FloatConverter : ITypeConverter
    {
        public object Convert(object valueToConvert)
        {
            if (valueToConvert == null || valueToConvert == DBNull.Value)
                return 0.0f;

            return System.Convert.ToSingle(valueToConvert);
        }

        public bool IsEqual(object value1, object value2)
        {
            var v1 = (float)Convert(value1);
            var v2 = (float)Convert(value2);

            return v1 == v2;
        }
        
        public Type GetCompatibleType()
        {
            return typeof(float);
        }
    }
}
