namespace InputReader;

public record YesNoInputValue : CharInputValue
{
    // public constructor if it has additional methods, to test the methods
    public YesNoInputValue(char? Value): base(Value) { }

    // TODO: check if it's case insensitive
    public bool IsYes() => Value == Constants.Chars.YesLower || Value == Constants.Chars.YesUpper;
    public bool IsNo() => Value == Constants.Chars.NoLower || Value == Constants.Chars.NoUpper;

    protected internal override string DefaultErrorMessage => "Invalid value. Please enter either 'Y' or 'N'";
}