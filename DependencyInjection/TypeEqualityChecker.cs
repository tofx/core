using System;

namespace tofx.Core.DependencyInjection
{
    public class TypeEqualityChecker
    {
        public static bool CheckEqual(Type t1, Type t2)
        {
            if (t1.AssemblyQualifiedName == t2.AssemblyQualifiedName)
                return true;
            else
                return false;
        }

        public static bool CheckInstanceEqual(Type t1, Type t2)
        {
            return t1.Equals(t2);
        }
    }
}
