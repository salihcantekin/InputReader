using InputReader.Converters.CustomConverters;

namespace InputReader;

public record DateOnlyInputValue(CustomDateOnly? Value) : InputValue<CustomDateOnly?>(Value)
{

}