using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tofx.Core.DependencyInjection
{
    public class TypedParameter : Parameter
    {
        public Type Type { get; private set; }

        private object _value = null;

        public static TypedParameter From<T>(T value)
        {
            return new TypedParameter(typeof(T), value);
        }

        public TypedParameter(Type objectType, object value)
        {
            Type = objectType;
            _value = value;
        }

        public override bool CanProvideValue()
        {
            return _value != null;
        }

        public override object GetValue()
        {
            return _value;
        }
    }
}
