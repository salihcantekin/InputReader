namespace InputReader;

public record StringInputValue : InputValue<string>
{
    internal StringInputValue(string Value) : base(Value) { }

    public bool IsNullOrEmpty() => string.IsNullOrWhiteSpace(Value);
    public bool IsNull() => Value is null;
}