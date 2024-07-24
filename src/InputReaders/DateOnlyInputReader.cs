using InputReader.Converters;
using InputReader.Converters.CustomConverters;
using InputReader.InputReaders.BaseInputReaders;
using InputReader.InputReaders.Interfaces;

namespace InputReader.InputReaders;

public sealed class DateOnlyInputReader : BaseInputReader<CustomDateOnly?, DateOnlyInputValue>
{
    public static DateOnlyInputReader DateOnly(string message = null, string format = Constants.Format.DateFormat) => new(message, format);

    public DateOnlyInputReader(string message, string format = Constants.Format.DateFormat) : base(message)
    {
        WithDateOnlyValueConverter(format);
    }

    public DateOnlyInputReader() : this(null)
    {
    }

    public IInputReader<CustomDateOnly?, DateOnlyInputValue> WithDateOnlyValueConverter(string format = Constants.Format.DateFormat)
    {
        return this.With(builder =>
        {
            builder.WithCustomConverter(new DateOnlyValueConverter(format));
        });
    }
}