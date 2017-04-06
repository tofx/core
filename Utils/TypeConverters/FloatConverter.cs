using TOF.Core.Abstractions;
using System;

namespace TOF.Core.Utils.TypeConverters
{
    public class FloatConverter : ITypeConverter
    {
        public object Convert(object ValueToConvert)
        {
            if (ValueToConvert == null || ValueToConvert == DBNull.Value)
                return 0.0f;

            return System.Convert.ToSingle(ValueToConvert);
        }

        public bool IsEqual(object Value1, object Value2)
        {
            var v1 = (float)Convert(Value1);
            var v2 = (float)Convert(Value2);

            return v1 == v2;
        }
        
        public Type GetCompatibleType()
        {
            return typeof(float);
        }
    }
}
