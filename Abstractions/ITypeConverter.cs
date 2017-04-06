using System;

namespace TOF.Core.Abstractions
{
    public interface ITypeConverter
    {
        object Convert(object ValueToConvert);
        bool IsEqual(object Value1, object Value2);
        Type GetCompatibleType();
    }
}
