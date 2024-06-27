namespace InputReader;

public record YesNoInputValue : CharInputValue
{
    public bool IsYes() => Value == 'y' || Value == 'Y';
    public bool IsNo() => Value == 'n' || Value == 'N';

    public YesNoInputValue(char value) : base(value)
    {

    }
}