namespace TOF.Core.DependencyInjection
{
    public abstract class Parameter
    {
        public virtual bool CanProvideValue()
        {
            return false;
        }

        public abstract object GetValue();
    }
}
