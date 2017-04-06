namespace tofx.Core.Abstractions
{
    public interface IValidateAttribute
    {
        bool IsValid(object value);
    }
}
