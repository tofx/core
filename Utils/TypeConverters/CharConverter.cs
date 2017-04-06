using TOF.Core.Abstractions;
using System;

namespace TOF.Core.Utils.TypeConverters
{
    public class CharConverter : ITypeConverter
    {
        public object Convert(object ValueToConvert)
        {
            if (ValueToConvert == null || ValueToConvert == DBNull.Value)
                return (char)0x0;

            return System.Convert.ToChar(ValueToConvert);
        }

        public bool IsEqual(object Value1, object Value2)
        {
            char v1 = (char)Convert(Value1);
            char v2 = (char)Convert(Value2);

            return v1 == v2;
        }
        
        public Type GetCompatibleType()
        {
            return typeof(char);
        }
    }
}
