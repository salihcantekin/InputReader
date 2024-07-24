using InputReader.Converters;
using InputReader.Converters.CustomConverters;
using InputReader.InputReaders.BaseInputReaders;
using InputReader.InputReaders.Interfaces;

namespace InputReader.InputReaders;

public sealed class TimeOnlyInputReader : BaseInputReader<CustomTimeOnly?, TimeOnlyInputValue>
{
    public static TimeOnlyInputReader TimeOnly(string message = null, string format = Constants.Format.TimeFormat) => new(message, format);

    public TimeOnlyInputReader(string message, string format = Constants.Format.TimeFormat) : base(message)
    {
        WithTimeOnlyValueConverter(format);
    }

    public TimeOnlyInputReader() : this(null)
    {
    }

    public IInputReader<CustomTimeOnly?, TimeOnlyInputValue> WithTimeOnlyValueConverter(string format = Constants.Format.TimeFormat)
    {
        return this.With(builder =>
        {
            builder.WithCustomConverter(new TimeOnlyValueConverter(format));
        });
    }
}