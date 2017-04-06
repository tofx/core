using System.Collections.Generic;

namespace TOF.Core.Infrastructure
{
    public class Environment : Dictionary<string, object>
    {
        public static object sLock = new object();

        public new void Add(string Key, object Value)
        {
            lock (sLock)
            {
                if (base.ContainsKey(Key))
                    return;

                base.Add(Key, Value);
            }
        }

        public new void Remove(string Key)
        {
            lock (sLock)
            {
                base.Remove(Key);
            }
        }
    }
}
