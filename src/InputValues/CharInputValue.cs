namespace InputReader;

public record CharInputValue : InputValue<char?>
{
    internal CharInputValue(char? Value) : base(Value) { }
}