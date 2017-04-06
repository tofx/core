namespace TOF.Core.DependencyInjection
{
    public class NamedParameter : Parameter
    {
        private object _value = null;

        public string Name { get; private set; }

        public NamedParameter(string Name, object Value)
        {
            this.Name = Name;
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
