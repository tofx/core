using System.Collections.Generic;

namespace tofx.Core.Infrastructure
{
    public class Environment : Dictionary<string, object>
    {
        public static object SLock = new object();

        public new void Add(string key, object value)
        {
            lock (SLock)
            {
                if (ContainsKey(key))
                    return;

                base.Add(key, value);
            }
        }

        public new void Remove(string key)
        {
            lock (SLock)
            {
                base.Remove(key);
            }
        }
    }
}
