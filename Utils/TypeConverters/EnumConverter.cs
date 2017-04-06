using tofx.Core.Abstractions;
using System;

namespace tofx.Core.Utils.TypeConverters
{
    public class EnumConverter : ITypeConverter
    {
        private Type _enumType = null;

        public EnumConverter(Type enumType)
        {
            if (enumType == null)
                throw new ArgumentNullException("enumType", "E_ENUM_TYPE_NOT_FOUND");

            _enumType = enumType;
        }

        public object Convert(object valueToConvert)
        {
            return Convert(_enumType, valueToConvert);
        }

        public object Convert(Type enumType, object valueToConvert)
        {
            if (!enumType.IsEnum)
                throw new InvalidOperationException("ERROR_TYPE_IS_NOT_ENUMERATION");

            if (valueToConvert.GetType() == typeof(string))
                return System.Convert.ChangeType(Enum.Parse(enumType, valueToConvert.ToString()), enumType);

            long v = 0;

            if (valueToConvert.GetType() == typeof(byte))
                v = (long)((byte)valueToConvert);
            else if (valueToConvert.GetType() == typeof(byte[]))
            {
                byte[] data = (byte[])valueToConvert;

                if (data.Length < 4)
                    v = (long)BitConverter.ToInt16(data, 0);
                else if (data.Length < 8)
                    v = (long)BitConverter.ToInt32(data, 0);
                else
                    v = BitConverter.ToInt64(data, 0);
            }
            else
                v = (long)System.Convert.ChangeType(valueToConvert, typeof(long));

            return Enum.ToObject(enumType, v);
        }

        public bool IsEqual(object value1, object value2)
        {
            throw new NotImplementedException();
        }

        public bool Equals(Type enumType, object value1, object value2)
        {
            if (!enumType.IsEnum)
                throw new InvalidOperationException("ERROR_TYPE_IS_NOT_ENUMERATION");
            
            return
                System.Convert.ChangeType(Enum.Parse(enumType, value1.ToString()), enumType) ==
                System.Convert.ChangeType(Enum.Parse(enumType, value2.ToString()), enumType); 
        }
        
        public Type GetCompatibleType()
        {
            return typeof(Enum);
        }
    }
}
