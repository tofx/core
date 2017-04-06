using tofx.Core.Abstractions;
using System;
using System.Linq;

namespace tofx.Core.Utils.TypeConverters
{
    public class GuidConverter : ITypeConverter
    {
        public object Convert(object valueToConvert)
        {
            if (valueToConvert == null || valueToConvert == DBNull.Value || string.IsNullOrEmpty(valueToConvert.ToString()))
                return Guid.Empty;

            return new Guid(valueToConvert.ToString());
        }

        public bool IsEqual(object value1, object value2)
        {
            if (value1 == null && value2 == null)
                return true;
            else if (value1 == null || value2 == null)
                return false;

            var v1 = (Guid)Convert(value1);
            var v2 = (Guid)Convert(value2);

            return v1.ToByteArray().SequenceEqual(v2.ToByteArray());
        }

        public Type GetCompatibleType()
        {
            return typeof(Guid);
        }
    }
}
