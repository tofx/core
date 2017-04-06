using System;

namespace tofx.Core.Utils
{
    public class ParameterChecker
    {
        public static void NotNull(object item)
        {
            if (item == null)
                throw new ArgumentNullException("ValueNotAllowNull");
        }

        public static void NotNull(object item, string message)
        {
            if (item == null)
                throw new ArgumentNullException(message);
        }

        public static void NotNull(object item, Exception exception)
        {
            if (item == null)
                throw exception;
        }

        public static void NotNull<TException>(object item) where TException : Exception
        {
            if (item == null)
                throw (TException)Activator.CreateInstance(typeof(TException), "ValueNotAllowNull");
        }

        public static void NotNull<TException>(object item, string message) where TException : Exception
        {
            if (item == null)
                throw (TException)Activator.CreateInstance(typeof(TException), message);
        }

        public static void NotNullOrEmpty(string item)
        {
            if (string.IsNullOrEmpty(item))
                throw new ArgumentNullException("ValueNotAllowNull");
        }

        public static void NotNullOrEmpty(string item, string message)
        {
            if (string.IsNullOrEmpty(item))
                throw new ArgumentNullException("ValueNotAllowNull");
        }

        public static void NotNullOrEmpty(string item, Exception exception)
        {
            if (string.IsNullOrEmpty(item))
                throw exception;
        }

        public static void NotNullOrEmpty<TException>(string item) where TException : Exception
        {
            if (string.IsNullOrEmpty(item))
                throw (TException)Activator.CreateInstance(typeof(TException), "ValueNotAllowNull");
        }

        public static void NotNullOrEmpty<TException>(string item, string message) where TException : Exception
        {
            if (string.IsNullOrEmpty(item))
                throw (TException)Activator.CreateInstance(typeof(TException), message);
        }
    }
}
