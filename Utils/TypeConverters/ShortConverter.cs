using TOF.Core.Abstractions;
using System;

namespace TOF.Core.Utils.TypeConverters
{
    public class ShortConverter : ITypeConverter
    {
        public object Convert(object ValueToConvert)
        {
            if (ValueToConvert == null || ValueToConvert == DBNull.Value)
                return 0;

            return System.Convert.ToInt16(ValueToConvert);
        }

        public bool IsEqual(object Value1, object Value2)
        {
            var v1 = (short)Convert(Value1);
            var v2 = (short)Convert(Value2);

            return v1 == v2;
        }
        
        public Type GetCompatibleType()
        {
            return typeof(short);
        }
    }
}
