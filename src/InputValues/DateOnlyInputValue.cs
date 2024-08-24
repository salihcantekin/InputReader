using InputReader.Converters.CustomConverters;
using InputReader.InputReaders.Interfaces;

namespace InputReader;

public record DateOnlyInputValue(CustomDateOnly? Value) : InputValue<CustomDateOnly?>(Value), IInRangeCompatible<CustomDateOnly?>
{
}