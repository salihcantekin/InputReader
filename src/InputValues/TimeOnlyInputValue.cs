using InputReader.Converters.CustomConverters;

namespace InputReader;

public record TimeOnlyInputValue : InputValue<CustomTimeOnly>
{
    private new CustomTimeOnly Value { get; }
    
    public TimeOnlyInputValue(CustomTimeOnly value) : base(value)
    {
        Value = value;
    }
}