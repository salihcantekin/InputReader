using InputReader.Validators.BuiltInValidators.Internals;

namespace InputReader.Validators.BuiltInValidators;

internal class CharInputPreValidator : IPreValidator
{
    public bool IsValid(string value)
    {
        var v = new InternalValueValidator<string>
        {
            ValidatorFunc = s => s?.Length == 1
        };

        return v.IsValid(value);
    }
}