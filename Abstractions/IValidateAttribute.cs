namespace TOF.Core.Abstractions
{
    public interface IValidateAttribute
    {
        bool IsValid(object Value);
    }
}
