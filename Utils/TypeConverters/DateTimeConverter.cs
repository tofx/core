using tofx.Core.Abstractions;
using System;

namespace tofx.Core.Utils.TypeConverters
{
    public class DateTimeConverter : ITypeConverter
    {
        public object Convert(object valueToConvert)
        {
            if (valueToConvert == null || valueToConvert == DBNull.Value)
                return DateTime.MinValue;

            return System.Convert.ToDateTime(valueToConvert);
        }

        public bool IsEqual(object value1, object value2)
        {
            var v1 = (DateTime)Convert(value1);
            var v2 = (DateTime)Convert(value2);

            return v1 == v2;
        }
        
        public Type GetCompatibleType()
        {
            return typeof(DateTime);
        }
    }
}
