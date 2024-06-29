using InputReader.Converters.CustomConverters;

namespace InputReader;

public record TimeOnlyInputValue : InputValue<CustomTimeOnly>
{
    public TimeOnlyInputValue(CustomTimeOnly value) : base(value)
    {
        Value = value;
    }
}