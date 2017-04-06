using TOF.Core.Abstractions;
using System;

namespace TOF.Core.Utils.TypeConverters
{
    public class DateTimeConverter : ITypeConverter
    {
        public object Convert(object ValueToConvert)
        {
            if (ValueToConvert == null || ValueToConvert == DBNull.Value)
                return DateTime.MinValue;

            return System.Convert.ToDateTime(ValueToConvert);
        }

        public bool IsEqual(object Value1, object Value2)
        {
            var v1 = (DateTime)Convert(Value1);
            var v2 = (DateTime)Convert(Value2);

            return v1 == v2;
        }
        
        public Type GetCompatibleType()
        {
            return typeof(DateTime);
        }
    }
}
