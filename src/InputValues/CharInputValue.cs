using InputReader.InputReaders.Interfaces;

namespace InputReader;

public record CharInputValue(char? Value) : InputValue<char?>(Value), IInRangeCompatible<char?>
{
    public bool Is(char chr) => Value == chr;
}