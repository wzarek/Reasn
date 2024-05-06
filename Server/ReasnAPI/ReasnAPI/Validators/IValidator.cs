using System.ComponentModel.DataAnnotations;

namespace ReasnAPI.Validators
{
    public interface IValidator<T>
    {
        static abstract IEnumerable<ValidationResult> Validate(T objectToValidate);
    }
}
