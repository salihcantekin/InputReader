using InputReader.Converters.CustomConverters;
using InputReader.InputReaders.Interfaces;

namespace InputReader;

public record TimeOnlyInputValue(CustomTimeOnly? Value) : InputValue<CustomTimeOnly?>(Value)
{
}