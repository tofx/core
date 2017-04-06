using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tofx.Core.DependencyInjection
{
    public class ValueParameter : Parameter
    {
        private object _value = null;

        public ValueParameter(object value)
        {
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
