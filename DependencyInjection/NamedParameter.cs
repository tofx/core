namespace tofx.Core.DependencyInjection
{
    public class NamedParameter : Parameter
    {
        private object _value = null;

        public string Name { get; private set; }

        public NamedParameter(string name, object value)
        {
            Name = name;
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
