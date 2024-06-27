namespace InputReader;

public record IntInputValue(int Value) : InputValue<int>(Value)
{
    public bool Is(int value) => value == base.Value;
    public bool IsZero() => Is(0);
    public bool IsOne() => Is(1);
    public bool IsTwo() => Is(2);
    public bool IsThree() => Is(3);
    public bool IsFour() => Is(4);
    public bool IsFive() => Is(5);
}