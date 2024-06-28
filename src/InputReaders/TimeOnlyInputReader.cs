using InputReader.Converters;
using InputReader.Converters.CustomConverters;
using InputReader.InputReaders.Interfaces;

namespace InputReader.InputReaders;

public sealed class TimeOnlyInputReader : BaseInputReader<CustomTimeOnly, TimeOnlyInputValue>
{
    public static TimeOnlyInputReader TimeOnly(string message = null) => new(message);

    public TimeOnlyInputReader(string message) : base(message)
    {
    }

    public TimeOnlyInputReader()
    {
    }

    public IInputReader<CustomTimeOnly, TimeOnlyInputValue> WithTimeOnlyValueConverter(string format = "HH:mm:ss")
    {
        WithValueConverter(new TimeOnlyValueConverter(format));
        return this;
    }
}