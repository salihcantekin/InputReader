namespace InputReader;

public record StringInputValue : InputValue<string>
{
    public bool IsNullOrEmpty() => string.IsNullOrWhiteSpace(Value);
    public bool IsNull() => Value is null;

    public StringInputValue(string value) : base(value)
    {

    }
}