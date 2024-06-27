using System;

namespace InputReader.Validators.BuiltInValidators.Internals;
internal class InternalValueValidator<T> : IValidator<T>
{
    internal Func<T, bool> ValidatorFunc { get; set; }

    public bool IsValid(T value)
    {
        return ValidatorFunc?.Invoke(value) ?? true;
    }
}