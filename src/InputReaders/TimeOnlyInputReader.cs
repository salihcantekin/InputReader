using InputReader.Converters;
using InputReader.Converters.Constants;
using InputReader.Converters.CustomConverters;
using InputReader.InputReaders.Interfaces;

namespace InputReader.InputReaders;

public sealed class TimeOnlyInputReader : BaseInputReader<CustomTimeOnly?, TimeOnlyInputValue>
{
    public static TimeOnlyInputReader TimeOnly(string message = null, string format = RelatedConstant.Time) => new(message, format);

    public TimeOnlyInputReader(string message, string format = RelatedConstant.Time) : base(message)
    {
        WithTimeOnlyValueConverter(format);
    }

    public TimeOnlyInputReader() : this(null)
    {
    }

    public IInputReader<CustomTimeOnly?, TimeOnlyInputValue> WithTimeOnlyValueConverter(string format = RelatedConstant.Time)
    {
        return WithValueConverter(new TimeOnlyValueConverter(format));
    }
}