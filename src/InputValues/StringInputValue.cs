namespace InputReader;

public record StringInputValue(string Value) : InputValue<string>(Value)
{
    public bool IsNullOrEmpty() => string.IsNullOrWhiteSpace(Value);
    public bool IsNull() => Value is null;
}
