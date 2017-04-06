using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOF.Core.DependencyInjection
{
    public class TypedParameter : Parameter
    {
        public Type Type { get; private set; }

        private object _value = null;

        public static TypedParameter From<T>(T Value)
        {
            return new TypedParameter(typeof(T), Value);
        }

        public TypedParameter(Type ObjectType, object Value)
        {
            Type = ObjectType;
            _value = Value;
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
