using tofx.Core.Abstractions;
using System;

namespace tofx.Core.Utils.TypeConverters
{
    public class BooleanConverter : ITypeConverter
    {
        public object Convert(object valueToConvert)
        {
            if (valueToConvert == null || valueToConvert == DBNull.Value)
                return false;

            if (string.IsNullOrEmpty(valueToConvert.ToString()))
                return false;
            if (valueToConvert.ToString() == "0")
                return false;
            if (valueToConvert.ToString() == "1")
                return true;

            return System.Convert.ToBoolean(valueToConvert);
        }

        public bool IsEqual(object value1, object value2)
        {
            var v1 = (bool)Convert(value1);
            var v2 = (bool)Convert(value2);

            return v1 == v2;
        }

        public Type GetCompatibleType()
        {
            return typeof(bool);
        }
    }
}
