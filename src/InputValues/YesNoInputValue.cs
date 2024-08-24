namespace InputReader;

public record YesNoInputValue(char? Value) : CharInputValue(Value)
{
    // TODO: check if it's case insensitive
    public bool IsYes() => Value == Constants.Chars.YesLower || Value == Constants.Chars.YesUpper;
    public bool IsNo() => Value == Constants.Chars.NoLower || Value == Constants.Chars.NoUpper;

    protected internal override string DefaultErrorMessage => Constants.Message.YesNoErrorMessage;
}