namespace InputReader;

public record YesNoInputValue(char? Value) : CharInputValue(Value)
{
    public override bool IsValid => Value is 'y' or 'Y' || Value == 'n' || Value == 'N';
    
    public bool IsYes() => Value == 'y' || Value == 'Y';
    public bool IsNo() => Value == 'n' || Value == 'N';
    
    public override string ToString() => Value?.ToString() ?? string.Empty;
}
