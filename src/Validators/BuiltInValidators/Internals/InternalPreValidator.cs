namespace InputReader.Validators.BuiltInValidators.Internals;

internal class InternalPreValidator : IPreValidator
{
    internal InternalValueValidator<string> ValueValidator = new();

    public bool IsValid(string value)
    {
        return ValueValidator.IsValid(value);
    }
}