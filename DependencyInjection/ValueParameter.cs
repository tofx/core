using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOF.Core.DependencyInjection
{
    public class ValueParameter : Parameter
    {
        private object _value = null;

        public ValueParameter(object Value)
        {
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
