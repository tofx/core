using TOF.Core.Abstractions;
using System;

namespace TOF.Core.Utils.TypeConverters
{
    public class EnumConverter : ITypeConverter
    {
        private Type _enumType = null;

        public EnumConverter(Type EnumType)
        {
            if (EnumType == null)
                throw new ArgumentNullException("EnumType", "E_ENUM_TYPE_NOT_FOUND");

            this._enumType = EnumType;
        }

        public object Convert(object ValueToConvert)
        {
            return Convert(this._enumType, ValueToConvert);
        }

        public object Convert(Type EnumType, object ValueToConvert)
        {
            if (!EnumType.IsEnum)
                throw new InvalidOperationException("ERROR_TYPE_IS_NOT_ENUMERATION");

            if (ValueToConvert.GetType() == typeof(string))
                return System.Convert.ChangeType(Enum.Parse(EnumType, ValueToConvert.ToString()), EnumType);

            long v = 0;

            if (ValueToConvert.GetType() == typeof(byte))
                v = (long)((byte)ValueToConvert);
            else if (ValueToConvert.GetType() == typeof(byte[]))
            {
                byte[] data = (byte[])ValueToConvert;

                if (data.Length < 4)
                    v = (long)BitConverter.ToInt16(data, 0);
                else if (data.Length < 8)
                    v = (long)BitConverter.ToInt32(data, 0);
                else
                    v = BitConverter.ToInt64(data, 0);
            }
            else
                v = (long)System.Convert.ChangeType(ValueToConvert, typeof(long));

            return Enum.ToObject(EnumType, v);
        }

        public bool IsEqual(object Value1, object Value2)
        {
            throw new NotImplementedException();
        }

        public bool Equals(Type EnumType, object Value1, object Value2)
        {
            if (!EnumType.IsEnum)
                throw new InvalidOperationException("ERROR_TYPE_IS_NOT_ENUMERATION");
            
            return
                System.Convert.ChangeType(Enum.Parse(EnumType, Value1.ToString()), EnumType) ==
                System.Convert.ChangeType(Enum.Parse(EnumType, Value2.ToString()), EnumType); 
        }
        
        public Type GetCompatibleType()
        {
            return typeof(Enum);
        }
    }
}
