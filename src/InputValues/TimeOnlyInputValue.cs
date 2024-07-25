using InputReader.Converters.CustomConverters;

namespace InputReader;

public record TimeOnlyInputValue : InputValue<CustomTimeOnly?>
{
    internal TimeOnlyInputValue(CustomTimeOnly? Value) : base(Value) { }
}