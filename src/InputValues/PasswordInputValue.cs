namespace InputReader.InputValues;
public record PasswordInputValue : InputValue<string>
{
    internal PasswordInputValue(string Value) : base(Value) { }
}
