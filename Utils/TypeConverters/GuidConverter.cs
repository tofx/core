using TOF.Core.Abstractions;
using System;
using System.Linq;

namespace TOF.Core.Utils.TypeConverters
{
    public class GuidConverter : ITypeConverter
    {
        public object Convert(object ValueToConvert)
        {
            if (ValueToConvert == null || ValueToConvert == DBNull.Value || string.IsNullOrEmpty(ValueToConvert.ToString()))
                return Guid.Empty;

            return new Guid(ValueToConvert.ToString());
        }

        public bool IsEqual(object Value1, object Value2)
        {
            if (Value1 == null && Value2 == null)
                return true;
            else if (Value1 == null || Value2 == null)
                return false;

            var v1 = (Guid)Convert(Value1);
            var v2 = (Guid)Convert(Value2);

            return v1.ToByteArray().SequenceEqual(v2.ToByteArray());
        }

        public Type GetCompatibleType()
        {
            return typeof(Guid);
        }
    }
}
