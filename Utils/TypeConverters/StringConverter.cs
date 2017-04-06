using TOF.Core.Abstractions;
using System;

namespace TOF.Core.Utils.TypeConverters
{
    public class StringConverter : ITypeConverter
    {
        private StringComparison _comparsionMode = StringComparison.InvariantCultureIgnoreCase;

        public StringConverter(StringComparison ComparsionMode = StringComparison.InvariantCultureIgnoreCase)
        {
            _comparsionMode = ComparsionMode;
        }

        public object Convert(object ValueToConvert)
        {
            if (ValueToConvert == null || ValueToConvert == DBNull.Value)
                return string.Empty;

            return ValueToConvert.ToString();
        }

        public bool IsEqual(object Value1, object Value2)
        {
            if (Value1 == null && Value2 == null)
                return true;
            else if (Value1 == null || Value2 == null)
                return false;
            else
                return string.Compare(Value1.ToString(), Value2.ToString(), _comparsionMode) == 0;
        }

        public Type GetCompatibleType()
        {
            return typeof(string);
        }
    }
}
