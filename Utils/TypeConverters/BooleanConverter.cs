using TOF.Core.Abstractions;
using System;

namespace TOF.Core.Utils.TypeConverters
{
    public class BooleanConverter : ITypeConverter
    {
        public object Convert(object ValueToConvert)
        {
            if (ValueToConvert == null || ValueToConvert == DBNull.Value)
                return false;

            if (string.IsNullOrEmpty(ValueToConvert.ToString()))
                return false;
            else if (ValueToConvert.ToString() == "0")
                return false;
            else if (ValueToConvert.ToString() == "1")
                return true;
            else
                return System.Convert.ToBoolean(ValueToConvert);
        }

        public bool IsEqual(object Value1, object Value2)
        {
            bool v1 = (bool)Convert(Value1);
            bool v2 = (bool)Convert(Value2);

            return v1 == v2;
        }

        public Type GetCompatibleType()
        {
            return typeof(bool);
        }
    }
}
