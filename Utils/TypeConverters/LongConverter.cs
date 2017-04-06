using TOF.Core.Abstractions;
using System;

namespace TOF.Core.Utils.TypeConverters
{
    public class LongConverter : ITypeConverter
    {
        public object Convert(object ValueToConvert)
        {
            if (ValueToConvert == null || ValueToConvert == DBNull.Value)
                return 0;

            return System.Convert.ToInt64(ValueToConvert);
        }

        public bool IsEqual(object Value1, object Value2)
        {
            var v1 = (long)Convert(Value1);
            var v2 = (long)Convert(Value2);

            return v1 == v2;
        }

        public Type GetCompatibleType()
        {
            return typeof(long);
        }
    }
}
