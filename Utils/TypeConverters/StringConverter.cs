using tofx.Core.Abstractions;
using System;

namespace tofx.Core.Utils.TypeConverters
{
    public class StringConverter : ITypeConverter
    {
        private StringComparison _comparsionMode = StringComparison.InvariantCultureIgnoreCase;

        public StringConverter(StringComparison comparsionMode = StringComparison.InvariantCultureIgnoreCase)
        {
            _comparsionMode = comparsionMode;
        }

        public object Convert(object valueToConvert)
        {
            if (valueToConvert == null || valueToConvert == DBNull.Value)
                return string.Empty;

            return valueToConvert.ToString();
        }

        public bool IsEqual(object value1, object value2)
        {
            if (value1 == null && value2 == null)
                return true;
            else if (value1 == null || value2 == null)
                return false;
            else
                return string.Compare(value1.ToString(), value2.ToString(), _comparsionMode) == 0;
        }

        public Type GetCompatibleType()
        {
            return typeof(string);
        }
    }
}
