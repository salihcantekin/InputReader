using InputReader.Converters.CustomConverters;

namespace InputReader;

public record TimeOnlyInputValue(CustomTimeOnly? Value) : InputValue<CustomTimeOnly?>(Value)
{

}