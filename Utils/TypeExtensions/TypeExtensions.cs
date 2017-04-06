using System;
using System.Collections;
using System.Collections.Generic;

namespace tofx.Core.Utils.TypeExtensions
{
    public static class TypeExtensions
    {
        public static bool IsNullable(this Type type)
        {
            if (!type.IsValueType)
                return true; // ref-type
            if (Nullable.GetUnderlyingType(type) != null)
                return true; // Nullable<T>

            return false;
        }

        public static bool IsCollection(this Type type)
        {
            if (type.IsArray)
                return true;

            return typeof(IEnumerable).IsAssignableFrom(type);
        }

        public static bool IsIEnumerable(this Type type)
        {
            foreach (Type intType in type.GetInterfaces())
            {
                if (intType.IsGenericType && intType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    return true;
                else if (!intType.IsGenericType && intType == typeof(IEnumerable))
                    return true;
            }

            return false;
        }
    }
}
