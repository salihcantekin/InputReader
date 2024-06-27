using InputReader.Validators.BuiltInValidators.Internals;

namespace InputReader.Validators.BuiltInValidators;

internal class IntInputPreValidator : IPreValidator
{
    public bool IsValid(string value)
    {
        var v = new InternalValueValidator<string>
        {
            ValidatorFunc = s => int.TryParse(s, out _)
        };

        return v.IsValid(value);
    }
}