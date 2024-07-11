namespace InputReader;

public record YesNoInputValue(char? Value) : CharInputValue(Value)
{
    public bool IsYes() => Value == 'y' || Value == 'Y';
    public bool IsNo() => Value == 'n' || Value == 'N';
}