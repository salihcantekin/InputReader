using InputReader.Converters.CustomConverters;

namespace InputReader;

public record DateOnlyInputValue: InputValue<CustomDateOnly?>
{
    internal DateOnlyInputValue(CustomDateOnly? Value) : base(Value) { }
}