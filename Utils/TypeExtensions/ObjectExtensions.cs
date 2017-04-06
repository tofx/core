using System;

namespace tofx.Core.Utils.TypeExtensions
{
    public static class ObjectExtensions
    {
        public static bool IsNullOrDbNull(this object obj)
        {
            return (obj == null || obj == DBNull.Value);
        }
    }
}
