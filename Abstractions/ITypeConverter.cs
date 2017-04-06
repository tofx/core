using System;

namespace tofx.Core.Abstractions
{
    public interface ITypeConverter
    {
        object Convert(object valueToConvert);
        bool IsEqual(object value1, object value2);
        Type GetCompatibleType();
    }
}
