using InputReader.Converters.CustomConverters;

namespace InputReader;

public record DateOnlyInputValue : InputValue<CustomDateOnly>
{
    private new CustomDateOnly Value { get; }
    
    public DateOnlyInputValue(CustomDateOnly value) : base(value)
    {
        Value = value;
    }
}