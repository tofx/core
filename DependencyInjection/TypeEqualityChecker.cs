using System;

namespace TOF.Core.DependencyInjection
{
    public class TypeEqualityChecker
    {
        public static bool CheckEqual(Type T1, Type T2)
        {
            if (T1.AssemblyQualifiedName == T2.AssemblyQualifiedName)
                return true;
            else
                return false;
        }

        public static bool CheckInstanceEqual(Type T1, Type T2)
        {
            return T1.Equals(T2);
        }
    }
}
