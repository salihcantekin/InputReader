using InputReader.Converters;
using InputReader.Converters.CustomConverters;
using InputReader.InputReaders.Interfaces;

namespace InputReader.InputReaders;

public sealed class TimeOnlyInputReader : BaseInputReader<CustomTimeOnly, TimeOnlyInputValue>
{
    public static TimeOnlyInputReader TimeOnly(string message = null, string format = "HH:mm:ss") => new(message, format);

    public TimeOnlyInputReader(string message, string format = "HH:mm:ss") : base(message)
    {
        WithTimeOnlyValueConverter(format);
    }

    public TimeOnlyInputReader(): this(null)
    {
    }

    public IInputReader<CustomTimeOnly, TimeOnlyInputValue> WithTimeOnlyValueConverter(string format = "HH:mm:ss")
    {
        return WithValueConverter(new TimeOnlyValueConverter(format));
    }
}