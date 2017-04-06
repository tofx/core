using TOF.Core.Abstractions;
using System;

namespace TOF.Core.Utils.TypeConverters
{
    public class DoubleConverter : ITypeConverter
    {
        public object Convert(object ValueToConvert)
        {
            if (ValueToConvert == null || ValueToConvert == DBNull.Value)
                return 0.0d;

            return System.Convert.ToDouble(ValueToConvert);
        }

        public bool IsEqual(object Value1, object Value2)
        {
            var v1 = (double)Convert(Value1);
            var v2 = (double)Convert(Value2);

            return v1 == v2;
        }
        
        public Type GetCompatibleType()
        {
            return typeof(double);
        }
    }
}
